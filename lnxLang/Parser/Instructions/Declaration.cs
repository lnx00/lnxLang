using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Parser.Instructions
{
    internal class Declaration : IInstruction
    {
        private string _variable;
        private object _value;

        public Declaration(string variable, object value)
        {
            _variable = variable;
            _value = value;
        }

        public bool Execute()
        {
            throw new NotImplementedException();
        }
    }
}
