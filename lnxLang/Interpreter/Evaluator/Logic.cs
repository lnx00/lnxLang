using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lnxLang.Interpreter.Variables;

namespace lnxLang.Interpreter.Evaluator
{
    internal static class Logic
    {

        public static bool Evaluate(string statement, Memory memory)
        {
            // TODO: Properly imeplement this
            VBoolean boolVar = (VBoolean)memory.Variables[statement];
            return boolVar.Value;
        }

    }
}
