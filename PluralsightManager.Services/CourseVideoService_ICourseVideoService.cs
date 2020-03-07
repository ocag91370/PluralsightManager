using DecryptPluralSightVideos.Encryption;
using PluralsightManager.Models;
using PluralsightManager.Models.Models;
using PluralsightManager.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;

namespace PluralsightManager.Services
{
    public partial class CourseVideoService : ICourseVideoService
    {
        public bool Download(CourseModel course, List<FolderModel> folders)
        {
            var result = true;

            var orderedModules = course.Modules.OrderBy(m => m.Index);
            Parallel.ForEach(orderedModules, module => Download(module, folders.Where(folder => folder.ModuleName == module.Name).ToList()));

            return result;
        }

        public bool Download(ModuleModel module, List<FolderModel> folders)
        {
            var result = true;

            var orderedClips = module.Clips.OrderBy(c => c.Index);
            Parallel.ForEach(orderedClips, clip => Download(clip, folders.FirstOrDefault(folder => folder.ClipName == clip.Name)));

            return result;
        }

        public bool Download(ClipModel clip, FolderModel folder)
        {
            var inputFile = Path.Combine(_configuration.InputPath, _configuration.VideoFolder, folder.Input.Folder, folder.Input.Filename);
            var outputFile = Path.Combine(_configuration.OutputPath, folder.Output.Folder, $"{folder.Output.Filename}{ _configuration.VideoFileExtension}");

            if (!File.Exists(inputFile))
            {
                _consoleService.Log(LogType.Warning, $"Video file '{inputFile}' cannot be found");
                return false;
            }

            _consoleService.Log(LogType.Begin, $"Start to download video file '{inputFile}'");

            try
            {
                var iStream = Extract(inputFile);
                var video = Decrypt(iStream);
                var status = Save(video, outputFile);

                _consoleService.Log(LogType.End, $"Video file '{outputFile}' downloaded successfully");

                return status;
            }
            catch
            {
                _consoleService.Log(LogType.End, $"Video file '{outputFile}' download failed");
                return false;
            }
        }
    }
}
