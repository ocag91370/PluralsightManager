using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Services.Contracts
{
    public interface IFolderManager
    {
        List<FolderModel> CreateFolders(CourseModel courseModel);
    }
}