using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Interpreter.Variables
{
    internal class String : IVariable
    {

        public string Name { get; }
        private string _value;

        public String(string name, string value)
        {
            Name = name;
        }

        public object GetValue()
        {
            return _value;
        }
    }
}
