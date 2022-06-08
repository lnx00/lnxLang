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

        public string Name { get; set; }
        public string Args { get; set; }
        public ContentType Type { get; set; }
        public List<IInstruction> Instructions { get; set; }

        public Function(string name, string args, ContentType type, List<IInstruction> instructions)
        {
            Name = name;
            Args = args;
            Type = type;
            Instructions = instructions;
        }

    }
}
