using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Contracts
{
    public interface IPluralsightService
    {
        ResultModel<List<CourseModel>> DownloadAllCourses();

        ResultModel<CourseModel> DownloadCourse(string courseId);

        IEnumerable<CourseModel> GetAllCourses();
    }
}