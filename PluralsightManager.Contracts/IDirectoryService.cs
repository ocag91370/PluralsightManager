using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Contracts
{
    public interface IDirectoryService
    {
        bool Create(string path);

        void Delete(string path);

        string CleanFolderName(string folder);
    }
}