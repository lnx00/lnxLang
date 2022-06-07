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
        private List<string> _globalVars = new();

        public ParseResult Parse(string code)
        {
            _program = new ParseResult();

            _program.Instructions.AddRange(ParseCode(code, _globalVars));

            return _program;
        }

        /* Parses the given code into instructions */
        private List<IInstruction> ParseCode(string code, List<string> parentVars)
        {
            List<IInstruction> instructions = new();  // Instructions of the current code
            List<string> localVars = new(parentVars); // Variables of the current scope
            Reader reader = new(code);

            // Read the entire code
            while (reader.CanRead())
            {
                reader.SeekStart();                 // Go to current line start
                string keyword = reader.PeekWord(); // Peek the next word (Keyword)
                if (keyword == string.Empty)
                {
                    Logger.Warn("Skipping empty keyword");
                    continue;
                }

                string line = reader.ReadUntil(';');    // Read the current line

                /* Keywords that will contain sub-code { } */
                switch (keyword)
                {
                    case "if":
                    {
                        throw new NotImplementedException("if-statements have not been implemented");
                    }
                }

                /* Keywords that will end with the next semicolon */
                switch (keyword)
                {
                    case "global":
                    case "local":
                    {
                        instructions.Add(ParseDeclaration(line, localVars));
                        break;
                    }

                    default:
                    {
                        // Check if the keyword is a know variable
                        if (_globalVars.Contains(keyword) || localVars.Contains(keyword))
                        {
                                instructions.Add(ParseAssignment(line));
                                break;
                        }

                        // Check if the keyword is a method

                        // Debug keywords
                        if (keyword == "debug")
                        {
                            instructions.Add(ParseDebug(line));
                            break;
                        }

                        Logger.Error("Unknown keyword", reader.GetPosition(), keyword);
                        // TODO: We should return here
                        break;
                    }
                }
            }

            return instructions;
        }

        /* Parses a global or local variable declaration */
        private Declaration ParseDeclaration(string line, List<string> localVars)
        {
            Logger.Log("Parsing variable declaration...");
            Reader reader = new(line);

            string scope = reader.ReadWord();
            string name = reader.ReadWord();
            if (reader.ReadWord() != "->")
            {
                throw new SyntaxErrorException("Invalid syntax for declaration! Expected '->' in: " + line);
            }
            string type = reader.ReadWord();
            if (reader.ReadWord() != "=")
            {
                throw new SyntaxErrorException("Invalid syntax for declaration! Expected '=' in: " + line);
            }
            string value = reader.ReadWord();
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception("Variable value of '" + name + "' cannot be '" + value + "'");
            }

            // Get the true variable type
            var trueType = Declaration.GetContentType(type);
            if (trueType == ContentType.None)
            {
                throw new Exception("Invalid variable type: " + type);
            }

            // Get the true access scope
            var trueScope = Declaration.GetAccessScope(scope);
            switch (trueScope)
            {
                case AccessScope.Local:
                {
                    localVars.Add(name);
                    break;
                }

                case AccessScope.Global:
                {
                    _globalVars.Add(name);
                    break;
                }

                default:
                {
                    throw new Exception("Invalid access scope: " + type);
                }
            }

            Logger.Log("Parsed " + scope + " variable " + name + " with value " + value);
            return new Declaration(name, Declaration.GetContentType(type), value, Declaration.GetAccessScope(scope));
        }

        /* Parses a variable assignment */
        private Assignment ParseAssignment(string line)
        {
            Logger.Log("Parsing variable assignment...");
            Reader reader = new(line);

            string variable = reader.ReadWord();
            if (reader.ReadWord() != "=")
            {
                throw new SyntaxErrorException("Invalid syntax for assignment! Expected '=' in: " + line);
            }
            string value = reader.ReadWord();

            Logger.Log("Parsed assignment of " + variable + " with value " + value);
            return new Assignment(variable, value);
        }

        /* Parses the debug keyword */
        private Debug ParseDebug(string line)
        {
            Reader reader = new(line);
            reader.ReadWord();

            return new Debug(reader.ReadAll());
        }

    }
}
