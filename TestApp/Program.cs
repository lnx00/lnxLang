using lnxLang;

Console.WriteLine("Running lnxLang test...");

string testCode = @"
global a -> int = 1
global b -> bool = false
a = 3
";

Executor executor = new Executor();
Console.WriteLine(executor.Evaluate(testCode) ? "Execution was successful!" : "Execution failed!");