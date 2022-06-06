using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Parser.Instructions
{
    internal class Assignment : IInstruction
    {

        public string Variable { get; }
        public string Value { get; }

        public Assignment(string variable, string value)
        {
            Variable = variable;
            Value = value;
        }

    }
}
