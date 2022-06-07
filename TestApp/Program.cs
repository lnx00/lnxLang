using lnxLang;

Console.WriteLine("Running lnxLang test...");

string testCode = @"
global a -> int = 10;
global b -> int = 0;

while (a > 0) {
    b = b + 5;
    a = a - 1;
}

Console.Log('');
debug DUMP;
";

Executor executor = new Executor();
Console.WriteLine(executor.Evaluate(testCode) ? "Execution was successful!" : "Execution failed!");