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
    public partial class CourseTranscriptService : ICourseTranscriptService
    {
        public bool Download(List<FolderModel> folders, List<TranscriptModel> transcripts)
        {
            var result = true;

            Parallel.ForEach(folders, folder =>
            {
                var folderTranscripts = transcripts.Select(t => t.Clip.Name == folder.ClipName);
                if (transcripts.Any())
                    Download(folder, transcripts);
            });

            return result;
        }

        public bool Download(FolderModel folder, List<TranscriptModel> transcripts)
        {
            var inputPath = Path.Combine(_configuration.InputPath, _configuration.VideoFolder, folder.Input.Folder, folder.Input.Filename);
            var outputPath = Path.Combine(_configuration.OutputPath, folder.Output.Folder, folder.Output.Filename);

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
