using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lnxLang.Interpreter.Variables;
using lnxLang.Parser;
using lnxLang.Parser.Instructions;
using lnxLang.Utils;
using Expression = NCalc.Expression;

namespace lnxLang.Interpreter
{
    internal class Interpreter
    {
        private readonly Memory _memory = new();
        private int _currentInstruction;

        public bool Interprete(ParseResult parseResult)
        {
            _currentInstruction = 0;
            while (_currentInstruction < parseResult.Instructions.Count)
            {
                var instruction = parseResult.Instructions[_currentInstruction];
                DoInstruction(instruction);

                _currentInstruction++;
            }

            return true;
        }

        private void DoInstruction(IInstruction instruction)
        {
            if (instruction is Declaration declaration)
            {
                DoDeclaration(declaration);
                return;
            }

            if (instruction is Assignment assignment)
            {
                DoAssignment(assignment);
                return;
            }

            if (instruction is Condition condition)
            {
                 DoCondition(condition);
                 return;
            }

            if (instruction is Jump jump)
            {
                DoJump(jump);
                return;
            }

            if (instruction is Debug debug)
            {
                DoDebug(debug);
                return;
            }

            Logger.Warn("Unknown instruction: " + instruction.GetType());
        }

        private void DoDeclaration(Declaration declaration)
        {
            IVariable newVariable = IVariable.GetFromType(declaration.Type);

            if (declaration.Type is ContentType.Integer or ContentType.Float or ContentType.Bool)
            {
                // Solve logical/math expression
                Expression eval = new(declaration.Value)
                {
                    Parameters = Memory.GetSimple(_memory.Variables)
                };
                newVariable.SetValue(eval.Evaluate());
            }
            else
            {
                // Other declarations (string etc.)
                newVariable.SetValue(declaration.Value);
            }

            // TODO: Respect scope

            // Add the variable to the corresponding list
            _memory.Variables.Add(declaration.Variable, newVariable);
            Logger.Log("Declaration: " + declaration.Variable + " = " + declaration.Value);
        }

        private void DoAssignment(Assignment assignment)
        {
            if (!_memory.Variables.ContainsKey(assignment.Variable))
            {
                throw new Exception("Cannot assign unknown variable: " + assignment.Variable);
            }

            var variable = _memory.Variables[assignment.Variable];
            if (variable is VInteger or VFloat or VBoolean)
            {
                // Solve logical/math expression
                Expression eval = new(assignment.Value)
                {
                    Parameters = Memory.GetSimple(_memory.Variables)
                };
                variable.SetValue(eval.Evaluate());
            }
            else
            {
                // Other assignments (string etc.)
                variable.SetValue(assignment.Value);
            }

            Logger.Log("Assignment: " + assignment.Variable + " = " + assignment.Value);
        }

        private void DoCondition(Condition condition)
        {
            Expression eval = new(condition.Statement)
            {
                Parameters = Memory.GetSimple(_memory.Variables)
            };

            bool result = (bool)eval.Evaluate();
            if (!result)
            {
                _currentInstruction += condition.Size;
            }
        }

        private void DoJump(Jump jump)
        {
            _currentInstruction += jump.Offset - 1;
        }

        private void DoDebug(Debug debug)
        {
            switch (debug.Task)
            {
                case DebugTask.Dump:
                {
                    Logger.Log("### Variable dump ###");
                    foreach (var variable in _memory.Variables)
                    {
                        Logger.Log(variable.Key + " = " + variable.Value.GetValue());
                    }
                    Logger.Log("#####################");
                    break;
                }

                default:
                {
                    Logger.Warn("Unknown debug task");
                    break;
                }
            }
        }

    }
}
