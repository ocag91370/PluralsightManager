using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightManager.Models
{
    public class PluralsightConfiguration
    {
        public string OutputPath { get; set; }

        public string InputPath { get; set; }

        public string VideoFolder { get; set; }

        public string VideoFileExtension { get; set; }

        public string TranscriptFileExtension { get; set; }
    }
}
