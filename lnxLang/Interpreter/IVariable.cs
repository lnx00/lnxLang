using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Interpreter
{
    internal interface IVariable
    {

        public string Name { get; }

        public object GetValue();

    }
}
