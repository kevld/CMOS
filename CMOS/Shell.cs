using CMOS.Apps;
using CMOS.Common;
using CMOS.Extensions;
using CMOS.Framework.Abstract;
using CMOS.Framework.Interface;
using CMOS.Managers;
using CMOS.Ressources;
using Cosmos.HAL;
using System;
using System.IO;

namespace CMOS
{
    public class Shell : App
    {
        private readonly IDiskProperties _diskProperties;
        private readonly LanguageManager _lm;


        private bool _initialized = false;

        public Shell(IDiskProperties diskProperties) : base(Version.Major, Version.Minor, Version.Build)
        {
            _diskProperties = diskProperties;
            _lm = new LanguageManager(_diskProperties);
        }

        public override void About()
        {
            Console.WriteLine($"Shell {GetVersion()}");
            Console.WriteLine();
        }

        public override void Exit()
        {
            Console.Write(Translation.SHELL_HALT.Translate());
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

        public override void Help()
        {
            Console.WriteLine($"=== {Translation.MENU_LABEL_COMMANDS.Translate()} ===");
            Console.WriteLine($"About / a        : {Translation.SHELL_HELP_ABOUT.Translate()}");
            Console.WriteLine($"Exit / e         : {Translation.SHELL_HELP_EXIT.Translate()}");
            Console.WriteLine($"Help / ?         : {Translation.SHELL_HELP_HELP.Translate()}");
            Console.WriteLine($"del <{Translation.SHELL_HELP_DEL_FILE.Translate()}>       : {Translation.SHELL_HELP_DEL.Translate()}");
            Console.WriteLine($"ls               : {Translation.SHELL_HELP_LIST_FILES.Translate()}");
            Console.WriteLine();
            Console.WriteLine($"=== {Translation.MENU_LABEL_PROGRAMS.Translate()} ===");
            Console.WriteLine($"Clock / c        : {Translation.SHELL_HELP_PROGRAM_CLOCK.Translate()}");
            Console.WriteLine($"Disk / d         : {Translation.SHELL_HELP_PROGRAM_DISK.Translate()}");
            Console.WriteLine($"Text / t         : {Translation.SHELL_HELP_PROGRAM_TEXT.Translate()}");
            Console.WriteLine($"Todo / l         : {Translation.SHELL_HELP_PROGRAM_TODO.Translate()}");
            Console.WriteLine($"ctl              : {Translation.SHELL_HELP_PROGRAM_CONTROL_PANEL.Translate()}");
            Console.WriteLine();
        }

        public override void Run()
        {
            if (!_initialized)
            {
                Initialize();
            }

            DisplayMenu();
        }

        private void Initialize()
        {
            Console.WriteLine($"{Translation.SHELL_INITIALIZE.Translate()}");

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
                case "ctl":
                    ControlPanel panel = new(_diskProperties);
                    panel.Run();

                    Container.Instance.Language = panel.Language;
                    _lm.LoadTranslation();

                    break;
                default:
                    if (input.ToLower().StartsWith("del"))
                    {
                        string[] cmd = input.Split(' ');
                        if (cmd.Length != 2)
                        {
                            Console.WriteLine($"{Translation.ERR_SHELL_DEL_SYNTAX.Translate()}");
                        }

                        string fileName = cmd[1];
                        if (!fileName.StartsWith(_diskProperties.RootPath))
                        {
                            fileName = _diskProperties.RootPath + fileName;
                        }

                        if (!File.Exists(fileName))
                        {
                            Console.WriteLine($"{Translation.ERR_FILE_NOT_FOUND.Translate()}");
                        }
                        else
                        {
                            File.Delete(fileName);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{Translation.ERR_SHELL_UNKNOWN_COMMAND.Translate()}");
                    }

                    break;
            }
        }
    }
}
