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
        private readonly Memory _memory; // Program memory
        private readonly ParseResult _parseResult;
        private int _current = 0; // Current instruction

        public Interpreter(ParseResult parseResult)
        {
            _parseResult = parseResult;
            _memory = new Memory();
        }

        public Interpreter(ParseResult parseResult, Memory memory)
        {
            _parseResult = parseResult;
            _memory = memory;
        }

        public void Interprete()
        {
            Interprete(_parseResult.Instructions);
        }

        public void Interprete(List<IInstruction> instructions)
        {
            _current = 0;
            while (_current < instructions.Count)
            {
                var instruction = instructions[_current];
                DoInstruction(instruction);

                _current++;
            }
        }

        private void DoInstruction(IInstruction instruction)
        {
            switch (instruction)
            {
                case Declaration declaration:
                {
                    DoDeclaration(declaration);
                    return;
                }

                case Assignment assignment:
                {
                    DoAssignment(assignment);
                    return;
                }

                case Condition condition:
                {
                    DoCondition(condition);
                    return;
                }

                case Jump jump:
                {
                    DoJump(jump);
                    return;
                }

                case Call call:
                {
                    DoCall(call);
                    return;
                }

                case Debug debug:
                {
                    DoDebug(debug);
                    return;
                }

                default:
                {
                    Logger.Warn("Unknown instruction: " + instruction.GetType());
                    break;
                }
            }
        }

        /* Handle variable declarations */
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
            else if (declaration.Type is ContentType.String)
            {
                // Evaluate string expression
                newVariable.SetValue(Evaluator.Strings.Evaluate(declaration.Value, _memory));
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

        /* Handle variable assignments */
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
            else if (variable is VString)
            {
                // Evaluate string expression
                variable.SetValue(Evaluator.Strings.Evaluate(assignment.Value, _memory));
            }
            else
            {
                // Other assignments (string etc.)
                variable.SetValue(assignment.Value);
            }

            Logger.Log("Assignment: " + assignment.Variable + " = " + assignment.Value);
        }

        /* Handle conditions (if, while) */
        private void DoCondition(Condition condition)
        {
            Expression eval = new(condition.Statement)
            {
                Parameters = Memory.GetSimple(_memory.Variables)
            };

            // Skip the instructions if the condition is false
            bool result = (bool)eval.Evaluate();
            if (!result)
            {
                _current += condition.Size;
            }
        }

        /* Handle instruction jumps */
        private void DoJump(Jump jump)
        {
            _current += jump.Offset - 1;
        }

        /* Handle method calls */
        private void DoCall(Call call)
        {
            Stack<string> callStack = new(call.Path.Reverse());
            string instance = callStack.Pop();

            // Try to call native functions
            switch (instance)
            {
                case "Console":
                {
                    Default.Console.Run(callStack, call.Args);
                    return;
                }
            }

            // Try to call defined functions
            if (_parseResult.Functions.ContainsKey(instance))
            {
                var function = _parseResult.Functions[instance];
                Interpreter funcInterpreter = new(_parseResult, _memory);
                funcInterpreter.Interprete(function.Instructions);

                return;
            }

            throw new Exception("Cannot call function: " + instance);
        }

        /* Handle debug tasks */
        private void DoDebug(Debug debug)
        {
            switch (debug.Task)
            {
                case DebugTask.Dump:
                {
                    Logger.Debug("### Variable dump ###");
                    foreach (var variable in _memory.Variables)
                    {
                        Logger.Debug(variable.Key + " = " + variable.Value.GetValue());
                    }
                    Logger.Debug("#####################");
                    break;
                }

                case DebugTask.Stop:
                {
                    Logger.Debug("Stopping execution");
                    throw new Exception("Execution was manually stopped");
                }

                default:
                {
                    Logger.Debug("Unknown debug task");
                    break;
                }
            }
        }

    }
}
