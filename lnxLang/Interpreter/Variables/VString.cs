using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Interpreter.Variables
{
    internal class VString : IVariable
    {

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

        public object GetValue()
        {
            return Value;
        }

        public void Add(string b)
        {
            Value += b;
        }

    }
}
