using CMOS.Framework.Interface;

namespace CMOS.Apps
{
    public class Todo : IApp
    {
        private readonly IDiskProperties _diskProperties;

        private List<string> lines;

        private bool isRunning;
        private bool forceRefresh = false;

        private int indexSelection = 0;

        private ConsoleKeyInfo keyInfo;

        public Todo(IDiskProperties diskProperties)
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

        public void About()
        {
            Console.WriteLine("Shell v0.0.2");
            Console.WriteLine();
        }

        public void Exit()
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
                Console.WriteLine($"File saved : {fileName}");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while saving : {e.Message}");
            }
            finally
            {
                isRunning = false;
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void Help()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=== Help ===");
            Console.WriteLine("Alt + A : About");
            Console.WriteLine("Alt + E : Exit");
            Console.WriteLine("Alt + H : Display this help");
            Console.WriteLine();
            Console.WriteLine("Up/Down : Select todo task");
            Console.WriteLine("Space : Complete task");
            Console.WriteLine("Enter : New task");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        public void Run()
        {
            while (isRunning)
            {
                if (forceRefresh)
                {
                    forceRefresh = false;
                    DrawTitle("Todo list");
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
