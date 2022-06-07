using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Interpreter.Evaluator
{
    internal static class Expression
    {

        public static int Evaluate(string[] expression)
        {
            Stack<int> vStack = new();
            Stack<string> opStack = new();
            opStack.Push("(");

            int pos = 0;
            while (pos < expression.Length)
            {
                if (pos == expression.Length || expression[pos] == ")")
                {
                    ProcessClosingParenthesis(vStack, opStack);
                    pos++;
                }

                else if (int.TryParse(expression[pos], out int n))
                {
                    pos = ProcessInputNumber(expression, pos, vStack);
                }

                else
                {
                    ProcessInputOperator(expression[pos], vStack, opStack);
                    pos++;
                }
            }

            return vStack.Pop();
        }

        private static void ProcessClosingParenthesis(Stack<int> vStack, Stack<string> opStack)
        {

            while (opStack.Peek() != "(")
            {
                ExecuteOperation(vStack, opStack);
            }

            opStack.Pop(); // Remove the opening parenthesis

        }

        private static int ProcessInputNumber(string[] exp, int pos, Stack<int> vStack)
        {
            while (pos < exp.Length && int.TryParse(exp[pos], out var n))
            {
                vStack.Push(n);
            }

            return pos;
        }

        private static void ProcessInputOperator(string op, Stack<int> vStack, Stack<string> opStack)
        {

            while (opStack.Count > 0 && OperatorCausesEvaluation(op, opStack.Peek()))
            {
                ExecuteOperation(vStack, opStack);
            }

            opStack.Push(op);

        }

        private static bool OperatorCausesEvaluation(string op, string prevOp)
        {

            bool evaluate = false;

            switch (op)
            {
                case "+":
                case "-":
                    evaluate = (prevOp != "(");
                    break;
                case "*":
                case "/":
                    evaluate = (prevOp == "*" || prevOp == "/");
                    break;
                case ")":
                    evaluate = true;
                    break;
            }

            return evaluate;

        }

        private static void ExecuteOperation(Stack<int> vStack, Stack<string> opStack)
        {

            int rightOperand = vStack.Pop();
            int leftOperand = vStack.Pop();
            string op = opStack.Pop();

            int result = 0;
            switch (op)
            {
                case "+":
                    result = leftOperand + rightOperand;
                    break;
                case "-":
                    result = leftOperand - rightOperand;
                    break;
                case "*":
                    result = leftOperand * rightOperand;
                    break;
                case "/":
                    result = leftOperand / rightOperand;
                    break;
            }

            vStack.Push(result);

        }

    }
}
