using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Interpreter.Evaluator
{
    internal class Strings
    {

        public static string Evaluate(string exp, Memory memory)
        {
            string result = "";

            string[] strings = exp.Split('+');
            foreach (var dirtyString in strings)
            {
                string str = dirtyString.Trim();
                if (str.StartsWith('"') && str.EndsWith('"'))
                {
                    string cleanStr = str[1..^1];
                    result += cleanStr;
                }
                else if (memory.Variables.ContainsKey(str))
                {
                    string value = (string)memory.Variables[str].GetValue();
                    result += value;
                }
            }

            return result;
        }

    }
}
