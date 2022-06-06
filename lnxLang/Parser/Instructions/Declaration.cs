using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Parser.Instructions
{
    internal class Declaration : IInstruction
    {
        internal enum ContentType
        {
            None, String, Bool, Integer, Float
        }

        public string Variable { get; }
        public ContentType Type { get; }
        public string Value { get; }

        public Declaration(string variable, ContentType type, string value)
        {
            Variable = variable;
            Type = type;
            Value = value;
        }
    }
}
