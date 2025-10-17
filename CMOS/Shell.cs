using Cosmos.HAL;
using System;

namespace CMOS
{
    public class Shell : IApp
    {
        private bool _initialized = false;

        public void About()
        {
            Console.WriteLine("Shell v0.0.1");
            Console.WriteLine();
        }

        public void Exit()
        {
            Console.Write("Shutting down computer.");
            System.Threading.Thread.Sleep(200);
            Console.Write(".");
            System.Threading.Thread.Sleep(200);
            Console.Write(".");
            System.Threading.Thread.Sleep(200);
            Console.Write(".");
            System.Threading.Thread.Sleep(200);
            Console.Write(".");
            System.Threading.Thread.Sleep(1000);

            Power.ACPIShutdown();
        }

        public void Help()
        {
            Console.WriteLine("=== Commands ===");
            Console.WriteLine("About / a    : About this program");
            Console.WriteLine("Exit / e     : Shutdown the computer");
            Console.WriteLine("Help / ?     : Display this menu");
            Console.WriteLine();
            Console.WriteLine("=== Programs ===");
            Console.WriteLine("Clock / c    : Start the clock");
            Console.WriteLine("Disk / d     : Display disk data");
            Console.WriteLine("Text / t     : Start the text editor");
            Console.WriteLine();
        }

        public void Run()
        {
            if (!_initialized)
            {
                Initialize();
            }

            DisplayMenu();
        }

        private void Initialize()
        {
            Console.WriteLine("Type \"Help\" or \"?\" to display the help");

            _initialized = true;
        }
        private void DisplayMenu()
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            ManageInput(input);
        }

        private void ManageInput(string input)
        {
            input = input.Trim().ToLower();
            switch (input)
            {
                case "help":
                case "?":
                    Help();
                    break;
                case "about":
                case "a":
                    About();
                    break;
                case "exit":
                case "e":
                    Exit();
                    break;
                case "clock":
                case "c":
                    Clock clock = new();
                    clock.Run();
                    break;
                case "disk":
                case "d":
                    Disk disk = new();
                    disk.Run();
                    break;
                case "text":
                case "t":
                    Text text = new();
                    text.Run();
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
    }
}
