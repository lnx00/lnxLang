using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Interpreter.Variables
{
    internal class VFloat : IVariable
    {
        
        public float Value { get; set; } = 0f;

        public VFloat() { }

        public VFloat(float value)
        {
            Value = value;
        }

        public void SetValue(string value)
        {
            Value = float.Parse(value);
        }

        public object GetValue()
        {
            return Value;
        }
    }
}
