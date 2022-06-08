using lnxLang.Parser;
using lnxLang.Utils;
using NCalc;

namespace lnxLang
{

    public class Executor
    {

        public bool Evaluate(string code)
        {
            try
            {
                // Parse the code to receive the instructions
                Logger.Log("Parsing code...");
                Parser.Parser parser = new();
                ParseResult parseResult = parser.Parse(code);

                // Execute the instructions
                Logger.Log("Interpreting program...");
                Interpreter.Interpreter interpreter = new();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }

    }
}