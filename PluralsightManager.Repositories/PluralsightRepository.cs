using PluralsightManager.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightManager.Repositories
{
    public class PluralsightRepository : IDisposable
    {
        private readonly IDbRepository _dbRepository;

        public PluralsightRepository()
        {
            _dbRepository = new DbRepository(new PluralsightDBContext());
        }

        /// <summary>
        /// Pluralsight repository constructor
        /// </summary>
        /// <param name="dbRepository"></param>
        public PluralsightRepository(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public void Dispose()
        {
            _dbRepository.Dispose();
        }

        /// <summary>
        /// Selects Product By Id
        /// </summary>
        public IEnumerable<CourseModel> GetAllCourses()
        {
            var courses = _dbRepository.GetAll<CourseEntity>(true);

            return courses.Select(entity => new CourseModel { Name = entity.Name, Title = entity.Title });
        }
    }
}
