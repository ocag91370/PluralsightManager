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
        private readonly PluralsightConfiguration _configuration;
        private readonly IConsoleService _consoleService;

        public CourseVideoService(PluralsightConfiguration configuration, IConsoleService consoleService)
        {
            _configuration = configuration;

            _consoleService = consoleService;
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
                _consoleService.Log(LogType.Error, $"An error occured during the extraction of the video : {ex.Message}");

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
                _consoleService.Log(LogType.Error, $"An error occured during the decryption of the video : {ex.Message}");

                return false;
            }
        }
    }
}
