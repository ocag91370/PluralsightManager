using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace PluralsightManager.Repositories.Entities
{
    public class TranscriptEntity
    {
        [Key]
        public double Id { get; set; }

        public double? StartTime { get; set; }

        public double? EndTime { get; set; }

        public string Text { get; set; }

        public double? ClipId { get; set; }

        public virtual ClipEntity Clip { get; set; }
    }
}
