using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Interpreter.Variables
{
    internal class VString : IVariable
    {

        public string Name { get; set; }
        public string Value { get; set; } = string.Empty;

        public VString() { }

        public VString(string value)
        {
            Value = value;
        }

        public void SetValue(string value)
        {
            Value = value;
        }

        public void Add(string b)
        {
            Value += b;
        }

    }
}
