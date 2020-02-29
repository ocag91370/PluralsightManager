using DecryptPluralSightVideos.Encryption;
using PluralsightManager.Models;
using PluralsightManager.Models.Models;
using PluralsightManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;

namespace PluralsightManager.Services
{
    public class VideoManager : IVideoManager
    {
        private readonly PluralsightConfiguration _configuration;

        private List<Task> _taskList = new List<Task>();
        private SemaphoreSlim _semaphore = new SemaphoreSlim(5);
        private object _semaphoreLock = new object();

        private readonly ConsoleColor _color_default;

        public VideoManager(PluralsightConfiguration configuration)
        {
            _configuration = configuration;

            _color_default = Console.ForegroundColor;
        }

        public bool DownloadCourse(List<FolderModel> folders)
        {
            var result = true;

            foreach (var folder in folders)
            {
                result &= DownloadClip(folder);
            }

            return result;
        }

        public bool DownloadClip(FolderModel folder)
        {
            var inputPath = Path.Combine(_configuration.InputPath, _configuration.VideoFolder, folder.InputFolder, folder.InputFilename);
            var outputPath = Path.Combine(_configuration.OutputPath, folder.OutputFolder, folder.OutputFilename);

            if (File.Exists(inputPath))
            {
                Console.WriteLine($"Start to Decrypt File '{inputPath}'");

                var iStream = ExtractVideo(inputPath);

                _semaphore.Wait();

                _taskList.Add(Task.Run((Action)(() =>
                {
                    DecryptVideo(iStream, outputPath);
                    lock (_semaphoreLock)
                        _semaphore.Release();
                })));

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Decryption File '{outputPath}' successfully");
                Console.ForegroundColor = _color_default;

                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"File '{inputPath}' cannot be found");
                Console.ForegroundColor = _color_default;

                return false;
            }
        }

        private IStream ExtractVideo(string inputPath)
        {
            try
            {
                var playingFileStream = new VirtualFileStream(inputPath);
                playingFileStream.Clone(out var curStream);

                return curStream;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occured : {ex.Message}");
                Console.ForegroundColor = _color_default;

                return null;
            }
        }

        private bool DecryptVideo(IStream curStream, string outputPath)
        {
            try
            {
                curStream.Stat(out var pstatstg, 0);
                IntPtr pcbRead = (IntPtr)0;
                int cbSize = (int)pstatstg.cbSize;
                byte[] numArray = new byte[cbSize];
                curStream.Read(numArray, cbSize, pcbRead);
                File.WriteAllBytes(outputPath, numArray);

                return true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occured : {ex.Message}");
                Console.ForegroundColor = _color_default;

                return false;
            }
        }
    }
}
