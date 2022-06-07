using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Interpreter.Variables
{
    internal class VBoolean : IVariable
    {

        public string Name { get; set; }
        public bool Value { get; set; } = false;

        public VBoolean() { }

        public void SetValue(string value)
        {
            Value = bool.Parse(value);
        }

        public VBoolean(bool value)
        {
            Value = value;
        }
    }
}
