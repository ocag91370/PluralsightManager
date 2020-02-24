using System.ComponentModel.DataAnnotations;

namespace PluralsightManager.Repositories
{
    public class CourseEntity
    {
        [Key]
        public string Name { get; set; }

        public string Title { get; set; }
    }
}
