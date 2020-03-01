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
        public bool DownloadCourse(List<FolderModel> folders)
        {
            var result = true;

            Parallel.ForEach(folders, folder => DownloadClip(folder));

            return result;
        }

        public bool DownloadClip(FolderModel folder)
        {
            var inputPath = Path.Combine(_configuration.InputPath, _configuration.VideoFolder, folder.InputFolder, folder.InputFilename);
            var outputPath = Path.Combine(_configuration.OutputPath, folder.OutputFolder, folder.OutputFilename);

            if (File.Exists(inputPath))
            {
                _consoleService.Log(LogType.Begin, $"Start to Decrypt File '{inputPath}'");

                var iStream = ExtractVideo(inputPath);
                DecryptVideo(iStream, outputPath);

                _consoleService.Log(LogType.End, $"Decryption File '{outputPath}' successfully");

                return true;
            }
            else
            {
                _consoleService.Log(LogType.Warning, $"File '{inputPath}' cannot be found");

                return false;
            }
        }
    }
}
