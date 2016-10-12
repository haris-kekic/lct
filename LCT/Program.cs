﻿using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using LCT.Analysis;
using LCT.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LCT
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitEvent = new ManualResetEvent(false);

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                exitEvent.Set();
            };

            Console.WriteLine("Welcome to LCT (Exit with Ctrl+C)");
            ExecEnvironment execEnvironment = new ConsoleExecEnvironment();
            do
            {
                Console.WriteLine();
                Console.Write("LCT>");
                string inputStatement = Console.ReadLine();
                execEnvironment.Execute(inputStatement);
            } while (true);

            exitEvent.WaitOne();
            Console.ResetColor();
            Console.Clear();
        }
    }
}
