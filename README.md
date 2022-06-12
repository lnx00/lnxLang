# lnxLang
A simple programming language written in C#

## Features
- Global and local variables (string, int, float, bool)
- if-Statements
- while-loops
- functions (without parameters)

## Example
```js
func LastIteration() -> void {
  Console.Print("End of while loop.");
}

global a -> int = 10;
global b -> int = 0;
while (a > 0) {
    b = b + 5;
    a = a - 1;
    
    if (a == 0) {
      LastIteration();
    }
}

global c -> string = "Hello";
c = c + " World!";
Console.Print("Hello, World!");

debug DUMP;
```
