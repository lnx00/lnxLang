using lnxLang;

Console.WriteLine("Running lnxLang test...");

string testCode = @"
global a -> int = 1
global b -> bool = true
a = 15
";

Executor executor = new Executor();
Console.WriteLine(executor.Evaluate(testCode) ? "Execution was successful!" : "Execution failed!");