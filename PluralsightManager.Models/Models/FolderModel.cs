using System;

namespace PluralsightManager.Models.Models
{
    public class FolderModel
    {
        public string CourseName { get; set; }

        public string ModuleName { get; set; }

        public string ClipName { get; set; }

        public FileModel Input { get; set; }

        public FileModel Output { get; set; }

        public string VideoExtension { get; set; }

        public string TranscriptExtension { get; set; }
    }
}