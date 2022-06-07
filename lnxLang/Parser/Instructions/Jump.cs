using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Parser.Instructions
{
    internal class Jump : IInstruction
    {

        public int Offset { get; set; }

        public Jump(int offset)
        {
            Offset = offset;
        }

    }
}
