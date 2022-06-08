using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lnxLang.Utils;

namespace lnxLang.Interpreter.Default
{
    internal static class Console
    {

        public static void Run(Stack<string> path, string args)
        {
            if (path.Count < 1)
            {
                throw new Exception("Cannot call class");
            }

            string method = path.Pop();
            switch (method)
            {
                case "Log":
                {
                    Log(args);
                    break;
                }

                case "Clear":
                {
                    Clear();
                    break;
                }
            }
        }

        private static void Log(string args)
        {
            Reader reader = new(args);
            string msg = reader.ReadString();

            System.Console.WriteLine(msg);
        }

        private static void Clear()
        {
            System.Console.Clear();
        }

    }
}
