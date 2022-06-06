using lnxLang;

Console.WriteLine("Running lnxLang test...");

string testCode = @"
global a -> int = 1
global b -> bool = false
";

Executor executor = new Executor();
if (executor.Evaluate(testCode))
{
    Console.WriteLine("Execution was successful!");
}
else
{
    Console.WriteLine("Execution failed!");
}