using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Contracts
{
    public interface IPluralsightService
    {
        ResultModel<CourseModel> DownloadCourse(string courseId);

        IEnumerable<CourseModel> GetAllCourses();
    }
}