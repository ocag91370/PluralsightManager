using System;
using System.Collections.Generic;

namespace PluralsightManager.Models.Models
{
    public class ModuleModel
    {
        public double Id { get; set; }

        public double Index { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TimeSpan Duration { get; set; }

        public string CourseName { get; set; }

        public string AuthorHandle { get; set; }

        public virtual ICollection<ClipModel> Clips { get; set; }
    }
}