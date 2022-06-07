using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Interpreter.Variables
{
    internal class VInteger : IVariable
    {
        
        public int Value { get; set; } = 0;

        public VInteger() { }

        public VInteger(int value)
        {
            Value = value;
        }

        public void SetValue(object value)
        {
            if (value is string str)
            {
                Value = int.Parse(str);
            }
            else
            {
                Value = (int)value;
            }
        }

        public object GetValue()
        {
            return Value;
        }

        public void Add(int b)
        {
            Value += b;
        }

        public void Substract(int b)
        {
            Value -= b;
        }
    }
}
