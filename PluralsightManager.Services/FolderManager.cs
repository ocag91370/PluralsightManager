using DecryptPluralSightVideos.Encryption;
using PluralsightManager.Models;
using PluralsightManager.Models.Models;
using PluralsightManager.Services.Contracts;
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
    public class FolderManager : IFolderManager
    {
        private readonly PluralsightConfiguration _configuration;

        private readonly List<char> _invalidCharacters = new List<char>();

        public FolderManager(PluralsightConfiguration configuration)
        {
            _configuration = configuration;

            _invalidCharacters.AddRange((IEnumerable<char>)Path.GetInvalidPathChars());
            _invalidCharacters.AddRange((IEnumerable<char>)Path.GetInvalidFileNameChars());
            _invalidCharacters.AddRange((IEnumerable<char>)new char[] { ':', '?', '"', '\\', '/' });
        }

        private string CleanPath(string path)
        {
            var result = new StringBuilder(path);

            foreach (var invalidCharacter in _invalidCharacters)
                result = result.Replace(invalidCharacter, '-');

            return result.ToString();
        }

        public string ModuleHash(string moduleName, string moduleAuthorName)
        {
            string s = moduleName + "|" + moduleAuthorName;
            using (var md5 = MD5.Create())
                return Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(s))).Replace('/', '_');
        }

        private List<FolderModel> ComputeFolders(CourseModel course)
        {
            var result = new List<FolderModel>();

            var courseFolder = CleanPath(course.Title);
            string coursePath = Path.Combine(_configuration.OutputPath, courseFolder);

            int modulePadding = (course.Modules.Count / 10).ToString().Length;
            int clipPadding = (course.Modules.Sum(c => c.Clips.Count)).ToString().Length;

            foreach (var module in course.Modules)
            {
                var moduleIndex = module.Index + 1;
                string moduleFolder = CleanPath($"{moduleIndex.ToString().PadLeft(modulePadding, '0')} - {module.Title}");
                var modulePath = Path.Combine(coursePath, moduleFolder);

                foreach (var clip in module.Clips)
                {
                    var clipIndex = clip.Index + 1;

                    var inputFolder = Path.Combine(course.Name, ModuleHash(module.Name, module.AuthorHandle));
                    var inputFilename = $"{clip.Name}.psv";
                    var outputFolder = Path.Combine(courseFolder, moduleFolder);
                    var outputFilename = CleanPath($"{clipIndex.ToString().PadLeft(clipPadding, '0')} - {clip.Title}.mp4");

                    result.Add(new FolderModel {
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

            // Purge the existing folder
            if (Directory.Exists(coursePath))
                Directory.Delete(coursePath, true);

            foreach (var folder in folders)
            {
                var modulePath = Path.Combine(_configuration.OutputPath, folder.OutputFolder);

                // Create the module folder
                if (! Directory.Exists(modulePath))
                    Directory.CreateDirectory(modulePath);

                if (!Directory.Exists(modulePath))
                {
                    //Directory.Delete(coursePath);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Download a course
        /// </summary>
        /// <param name="courseModel">The course model</param>
        public List<FolderModel> CreateFolders(CourseModel courseModel)
        {
            var folders = ComputeFolders(courseModel);
            CreateFolders(courseModel.Title, folders);

            return folders;
        }
    }
}
