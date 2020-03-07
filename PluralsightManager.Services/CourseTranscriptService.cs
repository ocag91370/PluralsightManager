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
        private readonly PluralsightConfiguration _configuration;
        private readonly IConsoleService _consoleService;

        public CourseTranscriptService(PluralsightConfiguration configuration, IConsoleService consoleService)
        {
            _configuration = configuration;

            _consoleService = consoleService;
        }

        private bool ExtractAndSave(List<TranscriptModel> transcripts, string outputFile)
        {
            try
            {
                int num = 1;

                StreamWriter streamWriter = new StreamWriter(outputFile);

                foreach (var transcript in transcripts)
                {
                    streamWriter.WriteLine(num++);
                    streamWriter.WriteLine($"{transcript.StartTime.ToString("hh\\:mm\\:ss\\,fff")} --> {transcript.EndTime.ToString("hh\\:mm\\:ss\\,fff")}");
                    streamWriter.WriteLine(transcript.Text);
                    streamWriter.WriteLine();
                }

                streamWriter.Close();

                return true;
            }
            catch (Exception ex)
            {
                _consoleService.Log(LogType.Error, $"An error occured during the extraction of the transcript : {ex.Message}");

                return false;
            }
        }
    }
}
