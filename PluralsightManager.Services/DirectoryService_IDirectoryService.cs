using PluralsightManager.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Text;

namespace PluralsightManager.Services
{
    public partial class DirectoryService : IDirectoryService
    {
        public bool Create(string path)
        {
            if (Directory.Exists(path))
            {
                _consoleService.Log(LogType.Warning, $"The directory '{path}' allready exists");
                return true;
            }

            try
            {
                Directory.CreateDirectory(path);
            }
            catch (UnauthorizedAccessException ex)
            {
                var security = Directory.GetAccessControl(path);
                security.AddAccessRule(new FileSystemAccessRule(@"Domain\Olivier", FileSystemRights.FullControl, AccessControlType.Allow));
                Directory.CreateDirectory(path, security);
            }
            catch (Exception ex)
            {
                _consoleService.Log(LogType.Error, $"Unable to create the directory '{path}' : {ex.Message}");
                return false;
            }

            if (!Directory.Exists(path))
            {
                _consoleService.Log(LogType.Error, $"Unable to create the directory '{path}'");
                return false;
            }

            _consoleService.Log(LogType.Done, $"The directory '{path}' has been successfully created");

            return true;

        }

        public bool Delete(string path)
        {
            if (!Directory.Exists(path))
            {
                _consoleService.Log(LogType.Warning, $"The directory '{path}' does not exist");
                return true;
            }

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
            try
            {
                Directory.Delete(path, true);
            }
            catch (UnauthorizedAccessException)
            {
                //var security = Directory.GetAccessControl(path);
                //security.AddAccessRule(new FileSystemAccessRule(@"Domain\Olivier", FileSystemRights.FullControl, AccessControlType.Allow));
                try
                {
                    Directory.Delete(path, true);
                }
                catch (Exception ex)
                {
                    _consoleService.Log(LogType.Error, $"Unable to delete the directory '{path}' : {ex.Message}");
                }
            }

            if (Directory.Exists(path))
            {
                _consoleService.Log(LogType.Error, $"Unable to delete the directory '{path}'");

                return false;

            }

            _consoleService.Log(LogType.Done, $"The directory '{path}' has been successfully deleted");

            return false;
        }

        public string CleanFolderName(string folder)
        {
            var invalidCharacters = new List<char> { ':', '?', '"', '\\', '/' };
            invalidCharacters.AddRange((IEnumerable<char>)Path.GetInvalidPathChars());
            invalidCharacters.AddRange((IEnumerable<char>)Path.GetInvalidFileNameChars());

            var result = new StringBuilder(folder);

            foreach (var character in invalidCharacters)
            {
                result = result.Replace(character, _substituteCharacter);
            }

            return result.ToString();
        }

        public int GetNbFiles(string path, string pattern)
        {
            return Directory.EnumerateFiles(path, pattern, SearchOption.AllDirectories).Count();
        }
    }
}
