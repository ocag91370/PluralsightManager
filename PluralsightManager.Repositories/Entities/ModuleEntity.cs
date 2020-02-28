using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace PluralsightManager.Repositories.Entities
{
    public class ModuleEntity
    {
        [Key]
        public double Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string AuthorHandle { get; set; }

        public string Description { get; set; }

        public double? DurationInMilliseconds { get; set; }

        public double? ModuleIndex { get; set; }

        public string CourseName { get; set; }
    }
}
