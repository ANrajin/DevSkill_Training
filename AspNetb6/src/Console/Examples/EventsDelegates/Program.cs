using System;

namespace EventsDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<string> printer = ConsolePrint;
            Func<int, int, int> logic = (a, b) => a + b;

            PrintSomething("Hello World", FormattedPrint);

            Console.WriteLine("Program ends");
        }

        static void ConsolePrint(string message)
        {
            Console.WriteLine(message);
        }

        static void FormattedPrint(string message)
        {
            Console.WriteLine("###" + message + "###");
        }

        static void PrintSomething(string message, Action<string> printer)
        {
            printer(message);
        }
    }
}
