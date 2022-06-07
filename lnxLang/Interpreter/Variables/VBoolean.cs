using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Interpreter.Variables
{
    internal class VBoolean : IVariable
    {
        
        public bool Value { get; set; } = false;

        public VBoolean() { }

        public void SetValue(object value)
        {
            if (value is string str)
            {
                Value = bool.Parse(str);
            }
            else
            {
                Value = (bool)value;
            }
        }

        public object GetValue()
        {
            return Value;
        }

        public VBoolean(bool value)
        {
            Value = value;
        }
    }
}
