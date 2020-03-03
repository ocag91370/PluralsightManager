using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Contracts
{
    public interface ICourseVideoService
    {
        bool DownloadCourse(CourseModel course, List<FolderModel> folders);

        bool DownloadModule(ModuleModel module, List<FolderModel> folders);

        bool DownloadClip(ClipModel clip, FolderModel folder);
    }
}