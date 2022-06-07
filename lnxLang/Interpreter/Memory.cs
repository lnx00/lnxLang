using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Interpreter
{
    internal class Memory
    {

        public Dictionary<string, IVariable> Variables = new();

        public static Dictionary<string, object> GetSimple(Dictionary<string, IVariable> vars)
        {
            Dictionary<string, object> simpleDict = new();
            foreach (var v in vars)
            {
                simpleDict[v.Key] = v.Value.GetValue();
            }

            return simpleDict;
        }

    }
}
