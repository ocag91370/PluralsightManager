using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Contracts
{
    public interface IPluralsightService
    {
        IEnumerable<CourseModel> GetAllCourses();

        IEnumerable<ModuleModel> GetAllModules();
        IEnumerable<ModuleModel> GetModulesOfCourse(string courseId);

        IEnumerable<ClipModel> GetAllClips();
        IEnumerable<ClipModel> GetClipsOfModule(double moduleId);
    }
}