using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Services.Contracts
{
    public interface IVideoManager
    {
        bool DownloadCourse(List<FolderModel> folders);

        bool DownloadClip(FolderModel folder);
    }
}