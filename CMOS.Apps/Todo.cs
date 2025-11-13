using CMOS.Common.Extensions;
using CMOS.Framework.Abstract;
using CMOS.Framework.Interface;
using CMOS.Ressources;

namespace CMOS.Apps
{
    public class Todo : App
    {
        private readonly IDiskProperties _diskProperties;

        private List<string> lines;

        private bool isRunning;
        private bool forceRefresh = false;

        private int indexSelection = 0;

        private ConsoleKeyInfo keyInfo;

        public Todo(IDiskProperties diskProperties) : base(Version.Major, Version.Minor, Version.Build)
        {
            if (diskProperties == null) throw new ArgumentNullException(nameof(diskProperties));

            lines = new List<string>();
            _diskProperties = diskProperties;
            isRunning = true;
            forceRefresh = true;

            Load();
        }

        private void Load()
        {
            string fileName = $"{_diskProperties.RootPath}t.odo";
            Console.WriteLine(fileName);
            Console.WriteLine(fileName);
            Console.WriteLine(fileName);
            Console.WriteLine(fileName);
            if (File.Exists(fileName))
            {
                string content = File.ReadAllText(fileName);
                Console.WriteLine(content);

                lines = content.Split('\n').ToList();

                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                lines = new List<string>();
            }
        }

        public override void About()
        {
            Console.WriteLine($"Shell {GetVersion()}");
            Console.WriteLine();
        }

        public override void Exit()
        {
            string fileName = $"{_diskProperties.RootPath}t.odo";

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
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{Translation.ERR_FILE_NOT_SAVED} : {e.Message}");
            }
            finally
            {
                isRunning = false;
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public override void Help()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"=== {Translation.GENERIC_PROGRAM_HELP_LABEL.Translate()} ===");
            Console.WriteLine($"Alt + A : {Translation.GENERIC_PROGRAM_HELP_ABOUT.Translate()}");
            Console.WriteLine($"Alt + E : {Translation.GENERIC_PROGRAM_HELP_EXIT.Translate()}");
            Console.WriteLine($"Alt + H : {Translation.GENERIC_PROGRAM_HELP_HELP.Translate()}");
            Console.WriteLine();
            Console.WriteLine(Translation.APP_TODO_KEY_ARROWS.Translate());
            Console.WriteLine(Translation.APP_TODO_KEY_SPACE.Translate());
            Console.WriteLine(Translation.APP_TODO_KEY_ENTER.Translate());
            Console.WriteLine();
            Console.WriteLine(Translation.GENERIC_PROGRAM_PRESS_ANY_KEY.Translate());
            Console.ReadKey(true);
        }

        public override void Run()
        {
            while (isRunning)
            {
                if (forceRefresh)
                {
                    forceRefresh = false;
                    DrawTitle(Translation.APP_TODO_APP_NAME.Translate());
                }

                Console.SetCursorPosition(0, 1);
                for (int i = 0; i < lines.Count; i++)
                {
                    if (i == indexSelection)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine(lines[i]);
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                keyInfo = Console.ReadKey(true);

                if ((keyInfo.Modifiers & ConsoleModifiers.Alt) != 0)
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.E: Exit(); break;
                        case ConsoleKey.A: About(); break;
                        case ConsoleKey.H: Help(); break;
                    }
                }
                else
                {
                    ManageSimpleKey(keyInfo.Key);
                }

            }
        }

        private void ManageSimpleKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    indexSelection--;
                    if (indexSelection < 0)
                        indexSelection = 0;
                    break;
                case ConsoleKey.DownArrow:
                    indexSelection++;
                    if (indexSelection > lines.Count - 1)
                    {
                        indexSelection = lines.Count - 1;
                        if (indexSelection < 0)
                            indexSelection = 0;
                    }
                    break;
                case ConsoleKey.Spacebar:
                    if (lines.Any() && indexSelection >= 0 && indexSelection <= lines.Count - 1)
                    {

                        lines.RemoveAt(indexSelection);
                        indexSelection--;
                        if (indexSelection < 0)
                            indexSelection = 0;
                        forceRefresh = true;
                    }
                    break;
                case ConsoleKey.Enter:
                    CreateNewTodoTask();
                    break;
                default:
                    break;
            }
        }

        private void DrawTitle(string title)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            int consoleWidth = Console.WindowWidth;
            title = $"=== {title} ===";

            int start = Math.Max(0, (consoleWidth - title.Length) / 2);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(new string(' ', consoleWidth));
            Console.SetCursorPosition(start, 0);
            Console.Write(title);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 1);
        }

        private void CreateNewTodoTask()
        {
            DrawTitle("Create a todo");

            Console.SetCursorPosition(0, 1);
            Console.BackgroundColor= ConsoleColor.Black;
            Console.ForegroundColor= ConsoleColor.White;
            Console.WriteLine("Type a todo, then enter to confirm :");

            string? input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                lines.Add(input);
            }

            forceRefresh = true;
        }
    }
}
