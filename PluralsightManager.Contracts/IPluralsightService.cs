using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Contracts
{
    public interface IPluralsightService
    {
        ResultModel<List<CourseModel>> DownloadAllCourses();

        ResultModel<CourseModel> DownloadCourseById(string courseId);

        ResultModel<CourseModel> DownloadCourseByTag(string courseTag);

        IEnumerable<CourseModel> GetAllCourses();
    }
}