using System;

namespace PluralsightManager.Models.Models
{
    public class ClipTranscriptModel
    {
        public double Id { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string Text { get; set; }

        public double ClipId { get; set; }

        public virtual ClipModel Clip { get; set; }
    }
}