using PluralsightManager.Contracts;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PluralsightManager.Services
{
    public partial class DirectoryService : IDirectoryService
    {
        public bool Create(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return Directory.Exists(path);
            }

            return true;
        }

        public void Delete(string path)
        {
            if (!Directory.Exists(path))
                return;

            var files = Directory.GetFiles(path);
            var dirs = Directory.GetDirectories(path);

            // Delete the files in the current directory
            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            // Then, delete the sub directories
            foreach (var dir in dirs)
            {
                Delete(dir);
            }

            // Finally, delete the base directory
            Directory.Delete(path, false);
        }

        public string CleanFolderName(string folder)
        {
            var invalidCharacters = new List<char> { ':', '?', '"', '\\', '/' };
            invalidCharacters.AddRange((IEnumerable<char>)Path.GetInvalidPathChars());
            invalidCharacters.AddRange((IEnumerable<char>)Path.GetInvalidFileNameChars());

            var substituteCharacter = '-';

            var result = new StringBuilder(folder);

            foreach (var character in invalidCharacters)
            {
                result = result.Replace(character, substituteCharacter);
            }

            return result.ToString();
        }
    }
}
