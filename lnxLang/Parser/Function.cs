using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lnxLang.Parser.Instructions;

namespace lnxLang.Parser
{
    internal class Function
    {
        public string Args { get; set; }
        public ContentType Type { get; set; }
        public List<IInstruction> Instructions { get; set; }

        public Function(string args, ContentType type, List<IInstruction> instructions)
        {
            Args = args;
            Type = type;
            Instructions = instructions;
        }

    }
}
