using lnxLang;
using System.IO;

Console.WriteLine("Running lnxLang test...");
string code = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/test.lnx");

/*string testCode = @"
global a -> int = 10;
global b -> int = 0;

while (a > 0) {
    b = b + 5;
    a = a - 1;
}

global c -> string = Hello;

Console.Log('Hello, World!');
debug DUMP;
";*/

Executor executor = new Executor();
Console.WriteLine(executor.Evaluate(code) ? "Execution was successful!" : "Execution failed!");