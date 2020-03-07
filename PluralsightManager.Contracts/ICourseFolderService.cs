using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Contracts
{
    public interface ICourseFolderService
    {
        List<FolderModel> CreateFolders(CourseModel courseModel);

        bool DeleteFolder(CourseModel courseModel);

        bool DeleteOutputFolder();

        int GetNbClipFiles(CourseModel courseModel);
    }
}