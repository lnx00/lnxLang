using lnxLang;

Console.WriteLine("Running lnxLang test...");

string testCode = @"
global a -> int = 1;
global b -> bool = false;
b = true;

if (b)
{
    a = 5;
    b = false;
    local c -> bool = true;
    if (c) { b = true; }
}

global d -> int = 10 + a;
d = a;
debug DUMP;
d = d + 1;
debug DUMP;
";

Executor executor = new Executor();
Console.WriteLine(executor.Evaluate(testCode) ? "Execution was successful!" : "Execution failed!");