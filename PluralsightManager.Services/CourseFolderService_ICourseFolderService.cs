using DecryptPluralSightVideos.Encryption;
using PluralsightManager.Contracts;
using PluralsightManager.Models;
using PluralsightManager.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PluralsightManager.Services
{
    public partial class CourseFolderService : ICourseFolderService
    {
        /// <summary>
        /// Create all the folders of a course
        /// </summary>
        /// <param name="courseModel">The course model</param>
        /// <returns>List of the folder of the course</returns>
        public List<FolderModel> CreateFolders(CourseModel courseModel)
        {
            var folders = ComputeFolders(courseModel);
            CreateFolders(courseModel.Title, folders);

            return folders;
        }
    }
}
