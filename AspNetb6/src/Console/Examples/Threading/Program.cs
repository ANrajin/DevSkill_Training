using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Threading
{
    class Program
    {

        static void Main(string[] args)
        {
            var fc = new FileCopier();
            fc.OnBegin += (message) => Console.WriteLine(message);
            fc.OnComplete += PrintMessage;
            fc.OnComplete += (m, s) => Console.WriteLine("File Copy Done");

            var source = @"C:\Training\aspnet-b6\src\Console\Examples\Threading\Sample.mkv";
            var destination = @$"C:\Training\aspnet-b6\src\Console\Examples\Threading\Storage";

            var thread = new Thread(() => fc.Copy(source, destination));
            thread.Start();

            Console.WriteLine("Program ends");
        }

        static void PrintMessage(string message, string fileName)
        {
            Console.WriteLine(message);
            Console.WriteLine($"File Name is : {fileName}");
        }
    }
}
