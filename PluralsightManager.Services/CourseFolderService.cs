using DecryptPluralSightVideos.Encryption;
using PluralsightManager.Contracts;
using PluralsightManager.Models;
using PluralsightManager.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PluralsightManager.Services
{
    public partial class CourseFolderService : ICourseFolderService
    {
        private readonly int _folderLimit = 260;
        private readonly PluralsightConfiguration _configuration;
        private readonly IDirectoryService _directoryService;
        private readonly IConsoleService _consoleService;

        public CourseFolderService(PluralsightConfiguration configuration, IDirectoryService directoryService, IConsoleService consoleService)
        {
            _configuration = configuration;
            _directoryService = directoryService;

            _consoleService = consoleService;
        }

        private string ModuleHash(string moduleName, string moduleAuthorName)
        {
            string s = moduleName + "|" + moduleAuthorName;
            using (var md5 = MD5.Create())
                return Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(s))).Replace('/', '_');
        }

        private List<FolderModel> ComputeFolders(CourseModel course)
        {
            var result = new List<FolderModel>();

            foreach (var module in course.Modules)
            {
                var inputPath = GetModuleInputPath(course, module);
                var outputPath = GetModuleOutputPath(course, module);

                foreach (var clip in module.Clips)
                {
                    var inputFilename = GetClipInputFilename(course, module, clip);
                    var outputFilename = GetClipOutputFilename(course, module, clip);

                    result.Add(new FolderModel
                    {
                        CourseName = course.Name,
                        ModuleName = module.Name,
                        ClipName = clip.Name,
                        Input = new FileModel {
                            Path = inputPath,
                            Filename = inputFilename
                        },
                        Output = new FileModel {
                            Path = outputPath,
                            Filename = outputFilename
                        }
                    });
                }
            }

            return result;
        }

        private bool CreateFolders(List<FolderModel> folders)
        {
            foreach (var folder in folders)
            {
                var modulePath = folder.Output.Path;

                // Create the module folder
                try
                {
                    if (!_directoryService.Create(modulePath))
                        return false;
                }
                catch (PathTooLongException ex)
                {
                    _consoleService.Log(LogType.Error, $"Path '{folder}' exceeds the limit : {ex.Message}");
                }
                catch (Exception ex)
                {
                    _consoleService.Log(LogType.Error, $"An error occured during the creation of the folder '{folder}' : {ex.Message}");
                }
            }

            return true;
        }

        private string GetModuleInputPath(CourseModel course, ModuleModel module)
        {
            var path = Path.Combine(_configuration.InputPath, _configuration.VideoFolder, course.Name, ModuleHash(module.Name, module.AuthorHandle));

            return path;
        }

        private string GetClipInputFilename(CourseModel course, ModuleModel module, ClipModel clip)
        {
            var filename = $"{clip.Name}.psv";

            return filename;
        }

        private bool DeleteFolder(string courseFolder)
        {
            string coursePath = GetCourseOutputPath(courseFolder);

            // Delete the course, if exists
            return _directoryService.Delete(coursePath);
        }

        private string GetCourseOutputPath(CourseModel course)
        {
            return GetCourseOutputPath(course.Title);
        }

        private string GetCourseOutputPath(string courseFolder)
        {
            var folder = _directoryService.CleanFolderName(courseFolder);
            var path = Path.Combine(_configuration.OutputPath, folder);

            return path;
        }

        private string GetModuleOutputPath(CourseModel course, ModuleModel module)
        {
            var paddingLength = course.Modules.Count.ToString().Length;
            var index = (module.Index + 1).ToString().PadLeft(paddingLength, '0');
            string folder = _directoryService.CleanFolderName($"{index} - {module.Title}");

            var path = Path.Combine(GetCourseOutputPath(course), folder);

            return path;
        }

        private string GetClipOutputFilename(CourseModel course, ModuleModel module, ClipModel clip)
        {
            int paddingLength = course.Modules.Max(m => m.Clips.Count).ToString().Length;
            var index = (clip.Index + 1).ToString().PadLeft(paddingLength, '0');

            var filename = _directoryService.CleanFolderName($"{index} - {clip.Title}");

            var path = Path.Combine(GetModuleOutputPath(course, module), filename);
            var length = path.Length + Math.Max(_configuration.VideoFileExtension.Length, _configuration.TranscriptFileExtension.Length);
            if (length >= _folderLimit)
            {
                var delta = length - _folderLimit;
                filename = $"{filename.Substring(0, filename.Length - delta)}__";
            }

            return filename;
        }
    }
}
