using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Contracts
{
    public interface ICourseVideoService
    {
        bool Download(CourseModel course, List<FolderModel> folders);

        bool Download(ModuleModel module, List<FolderModel> folders);

        bool Download(ClipModel clip, FolderModel folder);
    }
}