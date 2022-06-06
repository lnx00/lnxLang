using lnxLang.Parser;
using lnxLang.Utils;

namespace lnxLang
{

    public class Executor
    {

        public bool Evaluate(string code)
        {
            try
            {
                // Parse the code to receive the instructions
                Parser.Parser parser = new();
                ParseResult parseResult = parser.Parse(code);

                // Execute the instructions
                Interpreter.Interpreter interpreter = new();
                return interpreter.Interprete(parseResult);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }

    }
}