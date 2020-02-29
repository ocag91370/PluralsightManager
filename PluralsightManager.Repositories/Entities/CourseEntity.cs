using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace PluralsightManager.Repositories.Entities
{
    public class CourseEntity
    {
        [Key]
        public string Name { get; set; }

        public string Title { get; set; }

        public string ReleaseDate { get; set; }

        public string UpdatedDate { get; set; }

        public string Level { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public double? DurationInMilliseconds { get; set; }

        public int? HasTranscript { get; set; }

        public string AuthorsFullnames { get; set; }

        public string ImageUrl { get; set; }

        public string DefaultImageUrl { get; set; }

        public int? IsStale { get; set; }

        public string CachedOn { get; set; }

        public string UrlSlug { get; set; }

        public virtual ICollection<ModuleEntity> Modules { get; set; }
    }
}
