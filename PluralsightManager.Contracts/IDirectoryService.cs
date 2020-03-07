using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Contracts
{
    public interface IDirectoryService
    {
        bool Create(string path);

        bool Delete(string path);

        string CleanFolderName(string folder);

        int GetNbFiles(string path, string pattern);
    }
}