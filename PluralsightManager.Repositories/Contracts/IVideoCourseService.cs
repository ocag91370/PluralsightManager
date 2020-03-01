using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Contracts
{
    public interface IVideoCourseService
    {
        bool DownloadCourse(List<FolderModel> folders);

        bool DownloadClip(FolderModel folder);
    }
}