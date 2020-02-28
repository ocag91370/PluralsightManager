using PluralsightManager.Models.Models;
using PluralsightManager.Repositories.Contracts;
using PluralsightManager.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightManager.Repositories
{
    public class PluralsightRepository : IPluralsightRepository
    {
        private readonly IDatabaseRepository _dbRepository;

        public PluralsightRepository()
        {
            _dbRepository = new DatabaseRepository(new PluralsightDbContext());
        }

        /// <summary>
        /// Pluralsight repository constructor
        /// </summary>
        /// <param name="dbRepository"></param>
        public PluralsightRepository(IDatabaseRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public void Dispose()
        {
            _dbRepository.Dispose();
        }

        /// <summary>
        /// Get all courses
        /// </summary>
        public IEnumerable<CourseEntity> GetAllCourses()
        {
            var result = _dbRepository.GetAll<CourseEntity>(true);

            return result;
        }

        /// <summary>
        /// Get all modules
        /// </summary>
        public IEnumerable<ModuleEntity> GetAllModules()
        {
            var result = _dbRepository.GetAll<ModuleEntity>(true);

            return result;
        }
    }
}
