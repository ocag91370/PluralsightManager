using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Contracts
{
    public enum LogType
    {
        Begin,
        End,
        Error,
        Warning
    }

    public interface IConsoleService
    {
        void Log(LogType logType, string message);
    }
}