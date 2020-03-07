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

            var courseFolder = _directoryService.CleanFolderName(course.Title);
            string coursePath = GetOutputCoursePath(courseFolder);

            int modulePadding = course.Modules.Count.ToString().Length;
            int clipPadding = course.Modules.Max(module => module.Clips.Count).ToString().Length;

            foreach (var module in course.Modules)
            {
                var moduleIndex = (module.Index + 1).ToString().PadLeft(modulePadding, '0');
                string moduleFolder = _directoryService.CleanFolderName($"{moduleIndex} - {module.Title}");

                var outputFolder = Path.Combine(courseFolder, moduleFolder);
                //if (outputFolder.Length >= _folderLimit)
                //{
                //    var delta = _folderLimit - outputFolder.Length - _fileExtensionLength;
                //    outputFolder = $"{outputFolder.Substring(0, outputFolder.Length - delta)}__";
                //}

                foreach (var clip in module.Clips)
                {
                    var clipIndex = (clip.Index + 1).ToString().PadLeft(clipPadding, '0');

                    var outputFilename = _directoryService.CleanFolderName($"{clipIndex} - {clip.Title}");
                    var filename = Path.Combine(_configuration.OutputPath, outputFolder, outputFilename);
                    var length = filename.Length + Math.Max(_configuration.VideoFileExtension.Length, _configuration.TranscriptFileExtension.Length);

                    if (length >= _folderLimit)
                    {
                        var delta = length - _folderLimit;
                        outputFilename = $"{outputFilename.Substring(0, outputFilename.Length - delta)}__";
                    }

                    result.Add(new FolderModel
                    {
                        CourseName = course.Name,
                        ModuleName = module.Name,
                        ClipName = clip.Name,
                        Input = new FileModel
                        {
                            Folder = Path.Combine(course.Name, ModuleHash(module.Name, module.AuthorHandle)),
                            Filename = $"{clip.Name}.psv"
                        },
                        Output = new FileModel
                        {
                            //Folder = Path.Combine(courseFolder, moduleFolder),
                            //Filename = _directoryService.CleanFolderName($"{clipIndex.ToString().PadLeft(clipPadding, '0')} - {clip.Title}")
                            Folder = outputFolder,
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
                var modulePath = Path.Combine(_configuration.OutputPath, folder.Output.Folder);

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

        private bool DeleteFolder(string courseFolder)
        {
            string coursePath = GetOutputCoursePath(courseFolder);

            // Delete the course, if exists
            return _directoryService.Delete(coursePath);
        }

        private string GetOutputCoursePath(CourseModel courseModel)
        {
            return Path.Combine(_configuration.OutputPath, courseModel.Title);
        }

        private string GetOutputCoursePath(string courseFolder)
        {
            return Path.Combine(_configuration.OutputPath, courseFolder);
        }
    }
}
