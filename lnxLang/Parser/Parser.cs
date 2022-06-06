using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lnxLang.Parser.Instructions;
using lnxLang.Utils;

namespace lnxLang.Parser
{
    internal class Parser
    {

        private ParseResult _program = new();

        public ParseResult Parse(string code)
        {
            _program = new ParseResult();

            _program.Instructions.AddRange(ParseCode(code));

            return _program;
        }

        private List<IInstruction> ParseCode(string code)
        {
            List<IInstruction> instructions = new();
            Reader reader = new(code);

            // Read the entire code
            while (reader.CanRead())
            {
                reader.SeekStart();
                string keyword = reader.PeekWord();
                string line = reader.ReadLine();

                switch (keyword)
                {
                    case "global":
                    {
                        instructions.Add(ParseGlobal(line));
                        break;
                    }

                    default:
                    {
                        Logger.Error("Unknown keyword", reader.GetPosition(), keyword);
                        // TODO: We should return here
                        break;
                    }
                }
            }

            return instructions;
        }

        private Declaration ParseGlobal(string line)
        {
            Logger.Log("Parsing global declaration...");
            Reader reader = new(line);

            string scope = reader.ReadWord();
            string name = reader.ReadWord();
            if (reader.ReadWord() != "->")
            {
                throw new SyntaxErrorException("Invalid syntax for global declaration! Expected '->' in: " + line);
            }
            string type = reader.ReadWord();
            if (reader.ReadWord() != "=")
            {
                throw new SyntaxErrorException("Invalid syntax for global declaration! Expected '=' in: " + line);
            }
            string value = reader.ReadWord();

            // Get the true variable type
            var trueType = Declaration.GetContentType(type);
            if (trueType == ContentType.None)
            {
                throw new Exception("Invalid variable type: " + type);
            }

            // Get the true access scope
            var trueScope = Declaration.GetAccessScope(scope);
            if (trueScope == AccessScope.None)
            {
                throw new Exception("Invalid access scope: " + type);
            }

            Logger.Log("Parsed " + scope + " variable " + name + " with value " + value);
            return new Declaration(name, ContentType.None, value, AccessScope.Global);
        }

    }
}
