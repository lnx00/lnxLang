using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Parser
{
    internal class ParseResult
    {

        public List<IInstruction> Instructions = new();
        public Dictionary<string, Function> Functions = new();

    }
}
