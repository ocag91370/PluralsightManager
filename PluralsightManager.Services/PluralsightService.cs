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
        private readonly IConsoleService _consoleService;

        public PluralsightService(IPluralsightRepository pluralsightRepository, ICourseFolderService folderManager, ICourseVideoService courseVideoService, ICourseTranscriptService courseTranscriptService, IConsoleService consoleService)
        {
            _consoleService = consoleService;

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
            _courseFolderService.DeleteOutputFolder();

            var coursesResult = _pluralsightRepository
            .GetAllCourses()
            .ToList()
            .Map<List<CourseEntity>, List<CourseModel>>();

            Parallel.ForEach(coursesResult.Data, course => DownloadCourse(course));

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

            return DownloadCourse(courseResult.Data);
        }

        /// <summary>
        /// Download a course
        /// </summary>
        /// <param name="course">course to be downloaded</param>
        /// <returns>The status and datas of the course</returns>
        public ResultModel<CourseModel> DownloadCourse(CourseModel course)
        {
            _consoleService.Log(LogType.BeginCourse, $"Start to download the course '{course.Title}'");

            _courseFolderService.DeleteFolder(course);

            var folders = _courseFolderService.CreateFolders(course);

            var videoStatus = _courseVideoService.Download(course, folders);

            var transcriptsStatus = _courseTranscriptService.Download(course, folders);

            VerifyCourseDownload(course);

            _consoleService.Log(LogType.EndCourse, $"End of download of the course '{course.Title}'");

            return new ResultModel<CourseModel> { Data = course, Ok = true };
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

        private void VerifyCourseDownload(CourseModel course)
        {
            var nbClips = course.Modules.Sum(m => m.Clips.Count());
            var nbFiles = _courseFolderService.GetNbClipFiles(course);

            if (nbFiles < nbClips)
                _consoleService.Log(LogType.Error, $"Missing clip(s) after the download of the course '{course.Title}' : {nbClips - nbFiles} files");

            _consoleService.Log(LogType.Done, $"Status of the download of the course '{course.Title}' : [{nbClips} clips, {nbFiles} files]");
        }
    }
}
