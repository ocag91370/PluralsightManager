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
using System.Security.Cryptography;
using System.Text;

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

        private IStream Extract(string inputPath)
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

                throw ex;
            }
        }

        private byte[] Decrypt(IStream curStream)
        {
            try
            {
                curStream.Stat(out var pstatstg, 0);
                var pcbRead = (IntPtr)0;
                var cbSize = (int)pstatstg.cbSize;
                var numArray = new byte[cbSize];
                curStream.Read(numArray, cbSize, pcbRead);

                return numArray;
            }
            catch (Exception ex)
            {
                _consoleService.Log(LogType.Error, $"An error occured during the decryption of the video : {ex.Message}");

                throw ex;
            }
        }

        private bool Save(byte[] video, string outputPath)
        {
            try
            {
                File.WriteAllBytes(outputPath, video);

                return true;
            }
            catch (Exception ex)
            {
                _consoleService.Log(LogType.Error, $"An error occured during the saving of the video : {ex.Message}");

                throw ex;
            }
        }
    }
}
