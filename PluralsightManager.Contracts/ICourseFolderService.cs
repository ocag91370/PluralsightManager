using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Contracts
{
    public interface ICourseFolderService
    {
        List<FolderModel> CreateFolders(CourseModel courseModel);
    }
}