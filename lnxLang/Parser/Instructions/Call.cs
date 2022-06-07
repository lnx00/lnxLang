using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Parser.Instructions
{
    internal class Call : IInstruction
    {

        public string[] Path { get; set; }
        public string[] Args { get; set; }

        public Call(string[] path, string[] args)
        {
            Path = path;
            Args = args;
        }

    }
}
