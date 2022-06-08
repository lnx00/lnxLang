using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                string prefix = reader.Peek(2);
                
                /* Handle comments */

                // Single line comment
                if (prefix == "//")
                {
                    reader.ReadLine();
                    continue;
                }

                string keyword = reader.PeekWord(); // Peek the next word (Keyword)
                if (keyword == string.Empty)
                {
                    Logger.Warn("Skipping empty keyword");
                    continue;
                }

                /* Keywords that will contain sub-code { } */
                switch (keyword)
                {
                    case "if":
                    {
                        reader.ReadWord(); // Read 'if'
                        string statement = reader.ReadStack('(', ')'); // Read condition
                        string body = reader.ReadStack('{', '}'); // Read body

                        List<IInstruction> bodyInstructions = ParseCode(body, parentVars);
                        instructions.Add(new Condition(statement, bodyInstructions.Count));
                        instructions.AddRange(bodyInstructions);

                        Logger.Log("Parsed if-statement with condition: " + statement);
                        continue;
                    }

                    case "while":
                    {
                        reader.ReadWord();                             // Read 'while'
                        string statement = reader.ReadStack('(', ')'); // Read condition
                        string body = reader.ReadStack('{', '}');      // Read body

                        List<IInstruction> bodyInstructions = ParseCode(body, parentVars);
                        bodyInstructions.Add(new Jump(-bodyInstructions.Count - 1));

                        instructions.Add(new Condition(statement, bodyInstructions.Count));
                        instructions.AddRange(bodyInstructions);

                        Logger.Log("Parsed while-loop with condition: " + statement);
                        continue;
                    }
                }

                /* Keywords that will end with the next semicolon */
                string line = reader.ReadUntil(';'); // Read the current line
                switch (keyword)
                {
                    case "global":
                    case "local":
                    {
                        instructions.Add(ParseDeclaration(line, localVars));
                        continue;
                    }
                }

                /* Other keywords */

                // Debug keywords
                if (keyword == "debug")
                {
                    instructions.Add(ParseDebug(line));
                    continue;
                }

                // Check if the keyword is a known variable
                if (_globalVars.Contains(keyword) || localVars.Contains(keyword))
                {
                    instructions.Add(ParseAssignment(line));
                    continue;
                }

                // Check if the keyword is a method | TODO: Move the regex stuff to an Analyzer
                if (Regex.IsMatch(line, @"\S+\(.*\)"))
                {
                    instructions.Add(ParseCall(line));
                    continue;
                }

                Logger.Error("Unknown keyword", reader.GetPosition(), keyword);
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
            string value = reader.ReadAll();
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

            Logger.Log("Parsed " + scope + " variable " + name + " with value: " + value);
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
            string value = reader.ReadAll();

            Logger.Log("Parsed assignment of " + variable + " with value: " + value);
            return new Assignment(variable, value);
        }

        private Call ParseCall(string line)
        {
            Reader reader = new(line);
            string path = reader.ReadUntil('(');
            reader.Skip(-1);
            string args = reader.ReadStack('(', ')');
            string[] pathArray = path.Split('.');

            return new Call(pathArray, args);
        }

        /* Parses the debug keyword */
        private Debug ParseDebug(string line)
        {
            Reader reader = new(line);
            reader.ReadWord();
            string parameter = reader.ReadAll();

            return new Debug(Debug.GetDebugTask(parameter));
        }

    }
}
