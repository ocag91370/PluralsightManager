using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Contracts
{
    public interface IPluralsightService
    {
        IEnumerable<CourseModel> GetAllCourses();

        IEnumerable<ModuleModel> GetAllModules();
    }
}