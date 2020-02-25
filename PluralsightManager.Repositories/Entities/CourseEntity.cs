using System.ComponentModel.DataAnnotations;

namespace PluralsightManager.Repositories.Entities
{
    public class CourseEntity
    {
        [Key]
        public string Name { get; set; }

        public string Title { get; set; }
    }
}
