using System;

namespace PluralsightManager.Models.Models
{
    public class ClipModel
    {
        public double Id { get; set; }

        public double Index { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public TimeSpan Duration { get; set; }

        public double ModuleId { get; set; }
    }
}