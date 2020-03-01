using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Contracts
{
    public interface ICourseVideoService
    {
        bool DownloadCourse(List<FolderModel> folders);

        bool DownloadClip(FolderModel folder);
    }
}