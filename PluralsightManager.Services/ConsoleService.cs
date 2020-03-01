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
        private readonly ConsoleColor _color_default;

        public ConsoleService()
        {
            _color_default = Console.ForegroundColor;
        }

        public void Log(LogType logType, string message)
        {
            switch (logType)
            {
                case LogType.Begin:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case LogType.End:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;

                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

            Console.WriteLine(message);
            Console.ForegroundColor = _color_default;
        }
    }
}
