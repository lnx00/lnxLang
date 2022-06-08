using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnxLang.Parser.Instructions
{
    internal enum ContentType
    {
        Void, String, Bool, Integer, Float
    }

    internal enum AccessScope
    {
        None, Local, Global
    }

    internal class Declaration : IInstruction
    {
        public string Variable { get; }
        public ContentType Type { get; }
        public string Value { get; }
        public AccessScope Scope { get; }

        public Declaration(string variable, ContentType type, string value, AccessScope scope)
        {
            Variable = variable;
            Type = type;
            Value = value;
            Scope = scope;
        }

        public static ContentType GetContentType(string type)
        {
            return type switch
            {
                "string" => ContentType.String,
                "bool" => ContentType.Bool,
                "int" => ContentType.Integer,
                "float" => ContentType.Float,
                _ => ContentType.Void
            };
        }

        public static AccessScope GetAccessScope(string scope)
        {
            return scope switch
            {
                "local" => AccessScope.Local,
                "global" => AccessScope.Global,
                _ => AccessScope.None
            };
        }
    }
}
