using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Parser.Instructions
{
    internal class Debug : IInstruction
    {

        public string Message { get; }

        public Debug(string msg)
        {
            Message = msg;
        }

    }
}
