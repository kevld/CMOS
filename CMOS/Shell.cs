using CMOS.Apps;
using CMOS.Framework.Interface;
using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Sys = Cosmos.System;

namespace CMOS
{
    public class Shell : IApp
    {
        private IDiskProperties _diskProperties;

        private bool _initialized = false;

        public Shell(IDiskProperties diskProperties)
        {
            _diskProperties = diskProperties;
        }

        public void About()
        {
            Console.WriteLine("Shell v0.0.3");
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
            Console.WriteLine("About / a        : About this program");
            Console.WriteLine("Exit / e         : Shutdown the computer");
            Console.WriteLine("Help / ?         : Display this menu");
            Console.WriteLine("del <file>       : Delete specified file");
            Console.WriteLine("ls               : List files");
            Console.WriteLine();
            Console.WriteLine("=== Programs ===");
            Console.WriteLine("Clock / c        : Start the clock");
            Console.WriteLine("Disk / d         : Display disk data");
            Console.WriteLine("Text / t         : Start the text editor");
            Console.WriteLine("Todo / l         : Start the todo app");
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
                    Disk disk = new(_diskProperties);
                    disk.Run();
                    break;
                case "text":
                case "t":
                    Text text = new(_diskProperties);
                    text.Run();
                    break;
                case "todo":
                case "l":
                    Todo todo = new(_diskProperties);
                    todo.Run();
                    break;
                case "ls":
                    string[] files = Directory.GetFiles(_diskProperties.RootPath);
                    foreach (var file in files)
                    {
                        Console.WriteLine(file);
                    }
                    break;
                default:
                    if (input.ToLower().StartsWith("del"))
                    {
                        string[] cmd = input.Split(' ');
                        if (cmd.Length != 2)
                        {
                            Console.WriteLine("Syntax error. Usage : del <file name>");
                        }

                        string fileName = cmd[1];
                        if (!fileName.StartsWith(_diskProperties.RootPath))
                        {
                            fileName = _diskProperties.RootPath + fileName;
                        }

                        if (!File.Exists(fileName))
                        {
                            Console.WriteLine("This File does not exist");
                        }
                        else
                        {
                            File.Delete(fileName);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unknown command");

                    }

                    break;
            }
        }
    }
}
