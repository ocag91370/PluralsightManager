using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Contracts
{
    public interface ICourseVideoService
    {
        bool Download(List<FolderModel> folders);

        bool Download(FolderModel folder);
    }
}