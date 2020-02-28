using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace PluralsightManager.Repositories.Entities
{
    public class ClipEntity
    {
        [Key]
        public double Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public double? ClipIndex { get; set; }

        public double? DurationInMilliseconds { get; set; }

        public double? SupportsStandard { get; set; }

        public double? SupportsWidescreen { get; set; }

        public double? ModuleId { get; set; }
    }
}
