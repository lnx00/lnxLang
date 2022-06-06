using lnxLang.Parser;
using lnxLang.Utils;

namespace lnxLang
{

    public class Executor
    {

        public bool Evaluate(string programCode)
        {
            Parser.Parser parser = new();
            ParseResult? parseResult = parser.Parse(programCode);
            if (parseResult == null)
            {
                Logger.Error("Failed to parse program code");
                return false;
            }

            Interpreter.Interpreter interpreter = new();
            if (!interpreter.Interprete(parseResult))
            {
                Logger.Error("Failed to interprete program");
                return false;
            }

            return true;
        }

    }
}