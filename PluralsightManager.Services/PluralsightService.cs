using AutoMapper;
using PluralsightManager.Contracts;
using PluralsightManager.Models.Models;
using PluralsightManager.Repositories.Contracts;
using PluralsightManager.Repositories.Entities;
using PluralsightManager.Services.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightManager.Services
{
    public class PluralsightService : IPluralsightService
    {
        private readonly IPluralsightRepository _pluralsightRepository;
        private readonly ICourseFolderService _courseFolderService;
        private readonly ICourseVideoService _courseVideoService;
        private readonly ICourseTranscriptService _courseTranscriptService;

        public PluralsightService(IPluralsightRepository pluralsightRepository, ICourseFolderService folderManager, ICourseVideoService courseVideoService, ICourseTranscriptService courseTranscriptService)
        {
            _pluralsightRepository = pluralsightRepository;
            _courseFolderService = folderManager;
            _courseVideoService = courseVideoService;
            _courseTranscriptService = courseTranscriptService;
        }

        /// <summary>
        /// Download a course
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        /// <returns>The status and datas of the course</returns>
        public ResultModel<List<CourseModel>> DownloadAllCourses()
        {
                var coursesResult = _pluralsightRepository
                .GetAllCourses()
                .ToList()
                .Map<List<CourseEntity>, List<CourseModel>>();

            foreach (var course in coursesResult.Data)
            {
                var folders = _courseFolderService.CreateFolders(course);

                var downloadideosStatus = _courseVideoService.Download(folders);

                var transcripts = course.Modules.SelectMany(m => m.Clips.SelectMany(c => c.Transcripts));
                //var downloadTranscriptsStatus = _courseTranscriptService.Download(transcripts, folders);
            }

            return coursesResult;
        }

        /// <summary>
        /// Download a course
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        /// <returns>The status and datas of the course</returns>
        public ResultModel<CourseModel> DownloadCourse(string courseId)
        {
            var courseResult = _pluralsightRepository
                .GetCourse(courseId)
                .Map<CourseEntity, CourseModel>();

            var folders = _courseFolderService.CreateFolders(courseResult.Data);
            var downloadStatus = _courseVideoService.Download(folders);

            return courseResult;
        }

        /// <summary>
        /// Get a course
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        /// <returns>The status and datas of the course</returns>
        public ResultModel<CourseModel> GetCourse(string courseId)
        {
            var result = _pluralsightRepository
                            .GetCourse(courseId)
                            .Map<CourseEntity, CourseModel>();

            return result;
        }

        /// <summary>
        /// Get all courses
        /// </summary>
        public IEnumerable<CourseModel> GetAllCourses()
        {
            var courses = _pluralsightRepository.GetAllCourses().ToList();

            return courses.MapTo<List<CourseEntity>, List<CourseModel>>();
            //return courses.MapTo<List<CourseModel>>();
        }

        /// <summary>
        /// Get all modules
        /// </summary>
        public IEnumerable<ModuleModel> GetAllModules()
        {
            var modules = _pluralsightRepository.GetAllModules().ToList();

            return modules.MapTo<List<ModuleEntity>, List<ModuleModel>>();
        }

        /// <summary>
        /// Get all modules of a course
        /// </summary>
        /// <param name="courseId">Id of the course</param>
        public IEnumerable<ModuleModel> GetModulesOfCourse(string courseId)
        {
            var result = GetAllModules()
                            .Where(o => o.CourseName == courseId);

            return result;
        }

        /// <summary>
        /// Get all clips
        /// </summary>
        public IEnumerable<ClipModel> GetAllClips()
        {
            var result = _pluralsightRepository.GetAllClips().ToList();

            return result.MapTo<List<ClipEntity>, List<ClipModel>>();
        }

        /// <summary>
        /// Get all clips of a module
        /// </summary>
        /// <param name="moduleId">Id of the module</param>
        public IEnumerable<ClipModel> GetClipsOfModule(double moduleId)
        {
            var result = GetAllClips()
                            .Where(o => o.ModuleId == moduleId);

            return result;
        }
    }
}
