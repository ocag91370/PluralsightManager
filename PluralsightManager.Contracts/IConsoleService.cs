using PluralsightManager.Models.Models;
using System.Collections.Generic;

namespace PluralsightManager.Contracts
{
    public enum LogType
    {
        BeginCourse,
        EndCourse,
        Begin,
        End,
        Error,
        Warning,
        Done,
        Info
    }

    public interface IConsoleService
    {
        void Log(LogType logType, string message);
    }
}