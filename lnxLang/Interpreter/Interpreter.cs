using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lnxLang.Interpreter.Variables;
using lnxLang.Parser;
using lnxLang.Parser.Instructions;
using lnxLang.Utils;

namespace lnxLang.Interpreter
{
    internal class Interpreter
    {
        private Memory _memory = new();

        public bool Interprete(ParseResult parseResult)
        {
            for (int i = 0; i < parseResult.Instructions.Count; i++)
            {
                var instruction = parseResult.Instructions[i];
                if (!DoInstruction(instruction))
                {
                    throw new Exception("Failed to interprete instruction " + i);
                }
            }

            return true;
        }

        private bool DoInstruction(IInstruction instruction)
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
            IVariable newVariable = IVariable.GetFromType(declaration.Type);
            newVariable.SetValue(declaration.Value);

            // Get the variable content with the correct type
            /*switch (declaration.Type)
            {
                case ContentType.String:
                {
                    newVariable = new VString(declaration.Value);
                    break;
                }

                case ContentType.Bool:
                {
                    if (bool.TryParse(declaration.Value, out var boolValue))
                    {
                        newVariable = new VBoolean(boolValue);
                    }
                    else
                    {
                        throw new Exception("Failed to parse boolean value: " + declaration.Value);
                    }

                    break;
                }

                case ContentType.Integer:
                {
                    if (int.TryParse(declaration.Value, out var intValue))
                    {
                        newVariable = new VInteger(intValue);
                    }
                    else
                    {
                        throw new Exception("Failed to parse integer value: " + declaration.Value);
                    }

                    break;
                }

                case ContentType.Float:
                {
                    if (float.TryParse(declaration.Value, out var floatValue))
                    {
                        newVariable = new VFloat(floatValue);
                    }
                    else
                    {
                        throw new Exception("Failed to parse float value: " + declaration.Value);
                    }
                    break;
                }

                default:
                {
                    throw new Exception("Invalid variable content type for: " + declaration.Variable);
                }
            }*/

            // TODO: Respect scope

            // Add the variable to the corresponding list
            _memory.Variables.Add(declaration.Variable, newVariable);
            Logger.Log("Declaration: " + declaration.Variable + " = " + declaration.Value);

            return true;
        }

        private bool DoAssignment(Assignment assignment)
        {
            if (!_memory.Variables.ContainsKey(assignment.Variable))
            {
                throw new Exception("Cannot assign unknown variable: " + assignment.Variable);
            }

            var variable = _memory.Variables[assignment.Variable];
            variable.SetValue(assignment.Value);
            Logger.Log("Assignment: " + assignment.Variable + " = " + assignment.Value);

            return true;
        }

    }
}
