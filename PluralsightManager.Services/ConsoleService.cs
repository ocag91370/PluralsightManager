using PluralsightManager.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightManager.Services
{
    public class ConsoleService : IConsoleService
    {
        private readonly ConsoleColor _foregroundColor;
        private readonly ConsoleColor _backgroundColor;

        public ConsoleService()
        {
            _foregroundColor = ConsoleColor.White;
            _backgroundColor = ConsoleColor.Black;
        }

        public void Log(LogType logType, string message)
        {
            var backgroundColor = _backgroundColor;
            var foregroundColor = _foregroundColor;

            switch (logType)
            {
                case LogType.BeginCourse:
                    backgroundColor = ConsoleColor.Yellow;
                    foregroundColor = ConsoleColor.Black;
                    break;

                case LogType.EndCourse:
                    backgroundColor = ConsoleColor.Green;
                    foregroundColor = ConsoleColor.Black;
                    break;

                case LogType.Begin:
                    foregroundColor = ConsoleColor.Yellow;
                    break;

                case LogType.End:
                    foregroundColor = ConsoleColor.Green;
                    break;

                case LogType.Warning:
                    backgroundColor = ConsoleColor.Gray;
                    foregroundColor = ConsoleColor.Black;
                    break;

                case LogType.Error:
                    backgroundColor = ConsoleColor.Red;
                    foregroundColor = ConsoleColor.White;
                    break;

                case LogType.Done:
                    foregroundColor = ConsoleColor.White;
                    break;

                case LogType.Info:
                    foregroundColor = ConsoleColor.White;
                    break;
            }

            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;

            Console.WriteLine(message);

            Console.BackgroundColor= _backgroundColor;
            Console.ForegroundColor = _foregroundColor;
        }
    }
}
