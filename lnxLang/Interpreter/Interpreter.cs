using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lnxLang.Parser;
using lnxLang.Parser.Instructions;
using lnxLang.Utils;

namespace lnxLang.Interpreter
{
    internal class Interpreter
    {

        public bool Interprete(ParseResult parseResult)
        {
            for (int i = 0; i < parseResult.Instructions.Count; i++)
            {
                var instruction = parseResult.Instructions[i];
                if (!InterpreteInstruction(instruction))
                {
                    throw new Exception("Failed to interprete instruction " + i);
                }
            }

            return true;
        }

        private bool InterpreteInstruction(IInstruction instruction)
        {
            if (instruction is Declaration declaration)
            {
                return DoDeclaration(declaration);
            }

            if (instruction is Assignment assignment)
            {
                return DoAssignment(assignment);
            }

            return true;
        }

        private bool DoDeclaration(Declaration declaration)
        {
            return true;
        }

        private bool DoAssignment(Assignment assignment)
        {
            return true;
        }

    }
}
