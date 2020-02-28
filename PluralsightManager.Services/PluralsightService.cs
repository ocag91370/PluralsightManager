using AutoMapper;
using PluralsightManager.Contracts;
using PluralsightManager.Models.Models;
using PluralsightManager.Repositories.Contracts;
using PluralsightManager.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightManager.Services
{
    public class PluralsightService : IPluralsightService
    {
        private IPluralsightRepository _pluralsightRepository;

        public PluralsightService(IPluralsightRepository pluralsightRepository)
        {
            _pluralsightRepository = pluralsightRepository;
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
