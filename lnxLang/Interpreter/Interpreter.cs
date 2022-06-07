using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lnxLang.Interpreter.Evaluator;
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
                if (!DoInstruction(instruction))
                {
                    throw new Exception("Failed to interprete instruction " + _currentInstruction);
                }

                _currentInstruction++;
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

            if (instruction is Condition condition)
            {
                return DoCondition(condition);
            }

            if (instruction is Debug debug)
            {
                return DoDebug(debug);
            }

            Logger.Warn("Unknown instruction: " + instruction.GetType());
            return true;
        }

        private bool DoDeclaration(Declaration declaration)
        {
            IVariable newVariable = IVariable.GetFromType(declaration.Type);

            if (declaration.Type == ContentType.Integer || declaration.Type == ContentType.Float)
            {
                Expression eval = new Expression(declaration.Value);
                newVariable.SetValue(eval.Evaluate().ToString());
            }
            else
            {
                newVariable.SetValue(declaration.Value);
            }
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

        private bool DoCondition(Condition condition)
        {
            bool result = Logic.Evaluate(condition.Statement, _memory);
            if (!result)
            {
                _currentInstruction += condition.Size;
            }

            return true;
        }

        private bool DoDebug(Debug debug)
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

            return true;
        }

    }
}
