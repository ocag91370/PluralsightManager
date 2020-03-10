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
            CreateFolders(folders);

            return folders;
        }

        /// <summary>
        /// Delete all the folders of a course
        /// </summary>
        /// <param name="courseModel">The course model</param>
        /// <returns>The status</returns>
        public bool DeleteFolder(CourseModel courseModel)
        {
            return DeleteFolder(courseModel.Title);
        }

        /// <summary>
        /// Delete all the folders
        /// </summary>
        /// <returns>The status</returns>
        public bool DeleteOutputFolder()
        {
            return _directoryService.Delete(_configuration.OutputPath);
        }

        /// <summary>
        /// Get the number of files in a course folder
        /// </summary>
        /// <param name="courseModel">The course model</param>
        /// <returns>Number of files</returns>
        public int GetNbClipFiles(CourseModel courseModel)
        {
            var path = GetCourseOutputPath(courseModel.Title);
            var pattern = $"*{_configuration.VideoFileExtension}";

            return _directoryService.GetNbFiles(path, pattern);
        }
    }
}
