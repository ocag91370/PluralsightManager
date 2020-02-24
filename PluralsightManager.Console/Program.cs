using PluralsightManager.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluralsightManager.Console
{

    class Program
    {
        static void Main(string[] args)
        {
            List<CourseModel> courses;

            using (var repository = new PluralsightRepository())
            {
                courses = repository.GetAllCourses().ToList();
            }

            return;
        }
    }
}
