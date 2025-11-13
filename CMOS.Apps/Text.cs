using CMOS.Common.Extensions;
using CMOS.Framework.Abstract;
using CMOS.Framework.Interface;
using CMOS.Ressources;

namespace CMOS.Apps
{
    public class Text : App
    {
        private readonly IDiskProperties _diskProperties;

        private bool editing = true;
        private bool forceRefresh = false;
        private bool isSaved = true;

        private string? fileName = string.Empty;

        private List<string> lines = new List<string>();
        private int cursorX = 0;
        private int cursorY = 1;

        private ConsoleKeyInfo keyInfo;

        public Text(IDiskProperties diskProperties) : base(Version.Major, Version.Minor, Version.Build)
        {
            if(diskProperties == null) throw new ArgumentNullException(nameof(diskProperties));

            lines = new List<string> { "" };
            _diskProperties = diskProperties;
        }

        public override void About()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"=== {Translation.GENERIC_PROGRAM_HELP_ABOUT.Translate()} ===");
            Console.WriteLine($"Text {GetVersion()}");
            Console.WriteLine(Translation.APP_TEXT_APP_NAME.Translate());
            Console.WriteLine();
            Console.WriteLine(Translation.GENERIC_PROGRAM_PRESS_ANY_KEY.Translate());
            Console.ReadKey(true);
            forceRefresh = true;
        }

        public override void Help()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"=== {Translation.GENERIC_PROGRAM_HELP_LABEL.Translate()} ===");
            Console.WriteLine($"Alt + A : {Translation.GENERIC_PROGRAM_HELP_ABOUT.Translate()}");
            Console.WriteLine($"Alt + E : {Translation.GENERIC_PROGRAM_HELP_EXIT.Translate()}");
            Console.WriteLine($"Alt + O : {Translation.GENERIC_PROGRAM_HELP_OPEN.Translate()}");
            Console.WriteLine($"Alt + S {Translation.GENERIC_PROGRAM_OPTION_OR.Translate()} Ctrl + S : {Translation.GENERIC_PROGRAM_HELP_SAVE.Translate()}");
            Console.WriteLine($"Alt + H :{Translation.GENERIC_PROGRAM_HELP_HELP.Translate()}");
            Console.WriteLine();
            Console.WriteLine(Translation.APP_TEXT_KEY_ARROW.Translate());
            Console.WriteLine(Translation.APP_TEXT_KEY_ENTER.Translate());
            Console.WriteLine(Translation.APP_TEXT_KEY_BACKSPACE.Translate());
            Console.WriteLine();
            Console.WriteLine(Translation.GENERIC_PROGRAM_PRESS_ANY_KEY.Translate());
            Console.ReadKey(true);
            forceRefresh = true;
        }

        public override void Exit()
        {
            if (!isSaved)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Translation.ERR_FILE_NOT_SAVED.Translate());
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(Translation.APP_TEXT_EXIT_PRESS.Translate());
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Y");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(Translation.APP_TEXT_EXIT_PRESS_2.Translate());

                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Y)
                {
                    editing = false;
                    Console.Clear();
                }
                else
                {
                    forceRefresh = true;
                }
            }
            else
            {
                editing = false;
                Console.Clear();
            }
        }

        public override void Run()
        {
            DrawTitle();

            while (editing)
            {
                if (forceRefresh)
                {
                    forceRefresh = false;
                    DrawTitle();
                }

                Console.SetCursorPosition(0, 1);
                for (int i = 0; i < lines.Count; i++)
                {
                    Console.SetCursorPosition(0, i + 1);
                    Console.Write(lines[i].PadRight(Console.WindowWidth));
                }

                Console.SetCursorPosition(cursorX, cursorY);

                keyInfo = Console.ReadKey(true);

                if ((keyInfo.Modifiers & ConsoleModifiers.Alt) != 0)
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.S: Save(); break;
                        case ConsoleKey.O: Open(); break;
                        case ConsoleKey.E: Exit(); break;
                        case ConsoleKey.A: About(); break;
                        case ConsoleKey.H: Help(); break;
                    }
                }
                else if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0)
                {
                    if (keyInfo.Key == ConsoleKey.S)
                        Save();
                }
                else
                {
                    ManageSimpleKey(keyInfo.Key);
                    isSaved = false;
                }
            }
        }

        private void DrawTitle()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            int consoleWidth = Console.WindowWidth;
            string subTitle = string.IsNullOrEmpty(fileName) ? 
                Translation.APP_TEXT_NEW_FILE.Translate() : 
                Path.GetFileName(fileName);

            string title = $"=== Text - {subTitle} ===";

            int start = Math.Max(0, (consoleWidth - title.Length) / 2);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(new string(' ', consoleWidth));
            Console.SetCursorPosition(start, 0);
            Console.Write(title);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 1);
        }

        private void Open()
        {
            Console.Clear();
            List<string> files = Directory.GetFiles(_diskProperties.RootPath)
                .Where(x => x.EndsWith(".txt"))
                .ToList();

            if (!files.Any())
            {
                Console.WriteLine($"{Translation.APP_TEXT_NO_FILE.Translate()}. {Translation.GENERIC_PROGRAM_PRESS_ANY_KEY.Translate()}");
                Console.ReadKey(true);
                forceRefresh = true;
                return;
            }

            int selectedIndex = 0;
            bool selecting = true;

            while (selecting)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine($"=== {Translation.GENERIC_PROGRAM_HELP_OPEN.Translate()} ===");
                Console.ResetColor();

                for (int i = 0; i < files.Count; i++)
                {
                    string fileNameDisplay = Path.GetFileName(files[i]);
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"> {fileNameDisplay}");
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.WriteLine($"  {fileNameDisplay}");
                    }
                }

                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine(Translation.APP_TEXT_OPEN_NAVIGATE.Translate());

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : files.Count - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex < files.Count - 1) ? selectedIndex + 1 : 0;
                        break;

                    case ConsoleKey.Enter:
                        fileName = files[selectedIndex];
                        if (!fileName.StartsWith(_diskProperties.RootPath))
                            fileName = Path.Combine(_diskProperties.RootPath, fileName);

                        try
                        {
                            lines = File.ReadAllLines(fileName).ToList();
                            if (lines.Count == 0)
                                lines.Add(string.Empty);

                            cursorY = lines.Count;
                            cursorX = lines.Last().Length;
                            Console.Clear();
                            isSaved = true;
                        }
                        catch (Exception e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{Translation.ERR_FILE_NOT_OPENED.Translate()} : {e.Message}");
                            Console.ResetColor();
                            Console.ReadKey(true);
                        }

                        selecting = false;
                        break;

                    case ConsoleKey.Escape:
                        selecting = false;
                        break;
                }
            }

            forceRefresh = true;
        }

        private void Save()
        {
            Console.Clear();
            if (string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine($"{Translation.GENERIC_PROGRAM_FILE_NAME.Translate()} :");
                Console.Write("> ");
                do
                {
                    fileName = Console.ReadLine();
                } while (string.IsNullOrEmpty(fileName));
            }

            if (!fileName.EndsWith(".txt"))
                fileName += ".txt";
            if (!fileName.StartsWith(_diskProperties.RootPath))
                fileName = $"{_diskProperties.RootPath}{fileName}";

            try
            {
                string content = "";
                for (int i = 0; i < lines.Count; i++)
                {
                    content += lines[i];
                    if (i < lines.Count - 1)
                        content += "\n";
                }
                _diskProperties.CreateFile(fileName, content);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{Translation.GENERIC_PROGRAM_FILE_SAVED.Translate()} : {fileName}");

                isSaved = true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{Translation.ERR_FILE_NOT_SAVED.Translate()} : {e.Message}");
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(Translation.GENERIC_PROGRAM_PRESS_ANY_KEY.Translate());
                Console.ReadKey(true);
                forceRefresh = true;
            }
        }

        private void ManageSimpleKey(ConsoleKey key)
        {
            if (cursorY - 1 >= lines.Count)
                lines.Add("");

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (cursorY > 1)
                    {
                        cursorY--;
                        cursorX = Math.Min(cursorX, lines[cursorY - 1].Length);
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (cursorY < lines.Count)
                    {
                        cursorY++;
                        cursorX = Math.Min(cursorX, lines[cursorY - 1].Length);
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (cursorX > 0) cursorX--;
                    else if (cursorY > 1)
                    {
                        cursorY--;
                        cursorX = lines[cursorY - 1].Length;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (cursorX < lines[cursorY - 1].Length) cursorX++;
                    else if (cursorY < lines.Count)
                    {
                        cursorY++;
                        cursorX = 0;
                    }
                    break;

                case ConsoleKey.Enter:
                    string remainder = lines[cursorY - 1].Substring(cursorX);
                    lines[cursorY - 1] = lines[cursorY - 1].Substring(0, cursorX);
                    lines.Insert(cursorY, remainder);
                    cursorY++;
                    cursorX = 0;
                    break;

                case ConsoleKey.Backspace:
                    if (cursorX > 0)
                    {
                        lines[cursorY - 1] = lines[cursorY - 1].Remove(cursorX - 1, 1);
                        cursorX--;
                    }
                    else if (cursorY > 1)
                    {
                        string current = lines[cursorY - 1];
                        lines.RemoveAt(cursorY - 1);
                        cursorY--;
                        cursorX = lines[cursorY - 1].Length;
                        lines[cursorY - 1] += current;
                    }
                    break;

                default:
                    if (!char.IsControl(keyInfo.KeyChar))
                    {
                        lines[cursorY - 1] = lines[cursorY - 1].Insert(cursorX, keyInfo.KeyChar.ToString());
                        cursorX++;
                    }
                    break;
            }

            cursorX = Math.Min(cursorX, Console.WindowWidth - 1);
        }
    }
}
