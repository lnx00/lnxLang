using lnxLang.Parser;
using lnxLang.Utils;

namespace lnxLang
{

    public class Executor
    {

        public bool Evaluate(string code)
        {
            // Parse the code to receive the instructions
            Parser.Parser parser = new();
            ParseResult? parseResult = parser.Parse(code);
            if (parseResult == null)
            {
                Logger.Error("Failed to parse program code");
                return false;
            }

            // Execute the instructions
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