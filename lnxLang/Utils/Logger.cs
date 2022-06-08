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
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Debug(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("[Debug] " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("[Warning] " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[Error] " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(string message, int position)
        {
            Error(message + " at position " + position);
        }

        public static void Error(string message, int position, string code)
        {
            Error(message + " at position " + position + " in: " + code);
        }

    }
}
