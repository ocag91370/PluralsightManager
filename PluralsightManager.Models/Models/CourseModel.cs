using System;
using System.Collections.Generic;

namespace PluralsightManager.Models.Models
{
    public class CourseModel
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Level { get; set; }

        public bool HasTranscript { get; set; }

        public TimeSpan Duration { get; set; }

        public string UrlSlug { get; set; }

        public virtual ICollection<ModuleModel> Modules { get; set; }
    }
}