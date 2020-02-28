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

        public IEnumerable<CourseModel> GetAllCourses()
        {
            var courses = _pluralsightRepository.GetAllCourses().ToList();

            return courses.MapTo<List<CourseEntity>, List<CourseModel>>();
            //return courses.MapTo<List<CourseModel>>();
        }

        public IEnumerable<ModuleModel> GetAllModules()
        {
            var modules = _pluralsightRepository.GetAllModules().ToList();

            return modules.MapTo<List<ModuleEntity>, List<ModuleModel>>();
        }
    }
}
