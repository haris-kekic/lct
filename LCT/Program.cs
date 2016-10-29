using Antlr4.Runtime;
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

            Application.ExecutionEnvironment<string> execEnvironment = new Application.DefaultExecutionEnvironment();
            
            Console.WriteLine("Welcome to LCT (Exit with Ctrl+C)");
            
            do
            {
                Console.WriteLine();
                Console.Write("LCT>");
                string inputStatement = Console.ReadLine();

                if (inputStatement.Equals("quit", StringComparison.CurrentCultureIgnoreCase))
                {
                    break;
                }

                ///As this is a console app and we want the output to be textual
                Application.ExecutionContext<string> context = new Application.ExecutionContext<string>(inputStatement);
                execEnvironment.Execute(context);

                Console.WriteLine(context.Output);

            } while (true);

            exitEvent.WaitOne();
            Console.ResetColor();
            Console.Clear();
        }
    }
}
