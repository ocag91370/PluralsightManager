using PluralsightManager.Contracts;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PluralsightManager.Services
{
    public partial class DirectoryService : IDirectoryService
    {
        private readonly List<char> _invalidCharacters = new List<char>();
        private readonly char _substituteCharacter = '-';
        private readonly IConsoleService _consoleService;

        public DirectoryService(IConsoleService consoleService)
        {
            _consoleService = consoleService;

            _invalidCharacters.AddRange((IEnumerable<char>)Path.GetInvalidPathChars());
            _invalidCharacters.AddRange((IEnumerable<char>)Path.GetInvalidFileNameChars());
            _invalidCharacters.AddRange((IEnumerable<char>)new char[] { ':', '?', '"', '\\', '/' });
        }
    }
}
