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
        public bool DownloadCourse(CourseModel course, List<FolderModel> folders)
        {
            var result = true;

            var orderedModules = course.Modules.OrderBy(m => m.Index);
            Parallel.ForEach(orderedModules, module => DownloadModule(module, folders.Where(folder => folder.ModuleName == module.Name).ToList()));

            return result;
        }

        public bool DownloadModule(ModuleModel module, List<FolderModel> folders)
        {
            var result = true;

            var orderedClips = module.Clips.OrderBy(c => c.Index);
            Parallel.ForEach(orderedClips, clip => DownloadClip(clip, folders.FirstOrDefault(folder => folder.ClipName == clip.Name)));

            return result;
        }

        public bool DownloadClip(ClipModel clip, FolderModel folder)
        {
            var inputPath = Path.Combine(_configuration.InputPath, _configuration.VideoFolder, folder.Input.Folder, folder.Input.Filename);
            var outputPath = Path.Combine(_configuration.OutputPath, folder.Output.Folder, $"{folder.Output.Filename}{ _configuration.VideoFileExtension}");

            if (!File.Exists(inputPath))
            {
                _consoleService.Log(LogType.Warning, $"File '{inputPath}' cannot be found");
                return false;
            }

            _consoleService.Log(LogType.Begin, $"Start to download File '{inputPath}'");

            try
            {
                var iStream = ExtractVideo(inputPath);
                var video = DecryptVideo(iStream);
                var status = SaveVideo(video, outputPath);
            }
            catch
            {
                _consoleService.Log(LogType.End, $"File '{outputPath}' download failed");
                return false;
            }

            _consoleService.Log(LogType.End, $"File '{outputPath}' downloaded successfully");

            return true;
        }
    }
}
