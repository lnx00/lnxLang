using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lnxLang.Interpreter.Variables;
using lnxLang.Parser.Instructions;

namespace lnxLang.Interpreter
{
    internal interface IVariable
    {

        public string Name { get; set; }
        void SetValue(string value);

        static IVariable GetFromType(ContentType type)
        {
            return type switch
            {
                ContentType.String => new VString(),
                ContentType.Bool => new VBoolean(),
                ContentType.Integer => new VInteger(),
                ContentType.Float => new VFloat(),
                _ => throw new Exception("Invalid variable content type")
            };
        }

    }
}
