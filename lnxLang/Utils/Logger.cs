using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Utils
{
    internal class Logger
    {

        public static void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("[Log] " + message);
        }

        public static void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("[Warning] " + message);
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[Error] " + message);
        }

        public static void Error(string message, int line)
        {
            Error("[Error] " + message + " in line " + line);
        }

        public static void Error(string message, int line, string code)
        {
            Error("[Error] " + message + " in line " + line + " at: " + code);
        }

    }
}
