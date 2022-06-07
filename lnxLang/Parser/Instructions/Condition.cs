using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Parser.Instructions
{
    internal class Condition : IInstruction

    {
    public string Statement { get; set; }
    public int Size { get; set; }

    public Condition(string statement, int size)
    {
        Statement = statement;
        Size = size;
    }

    }
}
