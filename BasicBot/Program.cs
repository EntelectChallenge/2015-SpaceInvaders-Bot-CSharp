using System;
using System.Diagnostics;
using System.IO;
using BasicBot.Properties;

namespace BasicBot
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var stopwatch = Stopwatch.StartNew();
            
            RunBot(args);

            stopwatch.Stop();
            Console.WriteLine("[BOT]\tBot finished in {0} ms.", stopwatch.ElapsedMilliseconds);
        }

        private static void RunBot(string[] args)
        {
            if (args.Length != 1)
            {
                PrintUsage();
                Environment.Exit(1);
            }

            var outputPath = args[0];
            if (!Directory.Exists(outputPath))
            {
                PrintUsage();
                Console.WriteLine();
                Console.WriteLine("Error: Output folder \"" + outputPath + "\" does not exist.");
                Environment.Exit(1);
            }

            var bot = new BasicBot(outputPath);
            bot.Execute();
        }

        private static void PrintUsage()
        {
            Console.WriteLine("C# BasicBot usage: BasicBot.exe <outputFilename>");
            Console.WriteLine();
            Console.WriteLine("\toutputPath\tThe output folder where the match runner will output map and state files and look for the move file.");
        }
    }
}