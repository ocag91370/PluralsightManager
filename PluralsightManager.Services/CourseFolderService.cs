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
        private readonly PluralsightConfiguration _configuration;
        private readonly IDirectoryService _directoryService;

        public CourseFolderService(PluralsightConfiguration configuration, IDirectoryService directoryService)
        {
            _configuration = configuration;
            _directoryService = directoryService;
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
            string coursePath = Path.Combine(_configuration.OutputPath, courseFolder);

            int modulePadding = (course.Modules.Count / 10).ToString().Length;
            int clipPadding = (course.Modules.Sum(c => c.Clips.Count)).ToString().Length;

            foreach (var module in course.Modules)
            {
                var moduleIndex = module.Index + 1;
                string moduleFolder = _directoryService.CleanFolderName($"{moduleIndex.ToString().PadLeft(modulePadding, '0')} - {module.Title}");
                var modulePath = Path.Combine(coursePath, moduleFolder);

                foreach (var clip in module.Clips)
                {
                    var clipIndex = clip.Index + 1;

                    var inputFolder = Path.Combine(course.Name, ModuleHash(module.Name, module.AuthorHandle));
                    var inputFilename = $"{clip.Name}.psv";
                    var outputFolder = Path.Combine(courseFolder, moduleFolder);
                    var outputFilename = _directoryService.CleanFolderName($"{clipIndex.ToString().PadLeft(clipPadding, '0')} - {clip.Title}.mp4");

                    result.Add(new FolderModel
                    {
                        CourseName = course.Name,
                        ModuleName = module.Name,
                        ClipName = clip.Name,
                        InputFolder = inputFolder,
                        InputFilename = inputFilename,
                        OutputFolder = outputFolder,
                        OutputFilename = outputFilename
                    });
                }
            }

            return result;
        }

        private bool CreateFolders(string courseFolder, List<FolderModel> folders)
        {
            string coursePath = Path.Combine(_configuration.OutputPath, courseFolder);

            // Delete the course, if exists
            _directoryService.Delete(coursePath);

            foreach (var folder in folders)
            {
                var modulePath = Path.Combine(_configuration.OutputPath, folder.OutputFolder);

                // Create the module folder
                if (!_directoryService.Create(modulePath))
                    return false;
            }

            return true;
        }
    }
}
