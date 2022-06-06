using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Parser
{
    internal class Parser
    {

        private ParseResult _program = new();

        public ParseResult? Parse(string code)
        {
            _program = new ParseResult();

            return _program;
        }

    }
}
