using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Interpreter.Evaluator
{
    internal class ExpressionTree
    {

        class Node
        {
            public char data;
            public Node left, right;
            public Node(char data)
            {
                this.data = data;
                left = right = null;
            }
        }

    }
}
