using PluralsightManager.Models.Models;
using PluralsightManager.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightManager.Repositories.Contracts
{
    public interface IPluralsightRepository : IDisposable
    {
        CourseEntity GetCourse(string courseId);

        IEnumerable<CourseEntity> GetAllCourses();

        IEnumerable<ModuleEntity> GetAllModules();

        IEnumerable<ClipEntity> GetAllClips();
    }
}
