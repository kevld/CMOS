using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CMOS
{
    public class Text : IApp
    {
        private bool editing = true;
        private bool forceRefresh = false;
        private bool isSaved = true;

        private string fileName = string.Empty;

        private List<string> lines = new List<string>();
        private int cursorX = 0;
        private int cursorY = 1;

        private ConsoleKeyInfo keyInfo;

        public Text()
        {
            lines = new List<string> { "" };
        }

        public void About()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=== About ===");
            Console.WriteLine("Text v0.1.1");
            Console.WriteLine("Text editor");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            forceRefresh = true;
        }

        public void Help()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=== Help ===");
            Console.WriteLine("Alt + A : About");
            Console.WriteLine("Alt + E : Exit");
            Console.WriteLine("Alt + O : Open");
            Console.WriteLine("Alt + S or Ctrl + S : Save");
            Console.WriteLine("Alt + H : Display this help");
            Console.WriteLine();
            Console.WriteLine("Arrow : Move cursor");
            Console.WriteLine("Enter : New line");
            Console.WriteLine("Backspace : Remove character");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            forceRefresh = true;
        }

        public void Exit()
        {
            if (!isSaved)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("File not saved !");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Press ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Y");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" to exit without saving, or any key to go back.");

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

        public void Run()
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
            string subTitle = string.IsNullOrEmpty(fileName) ? "New file" : Path.GetFileName(fileName);
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
            List<string> files = Directory.GetFiles(@"0:\")
                .Where(x => x.EndsWith(".txt"))
                .ToList();

            if (!files.Any())
            {
                Console.WriteLine("No file found. Press any key to continue...");
                Console.ReadKey(true);
                forceRefresh = true;
                return;
            }

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("=== Open file ===");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            for (int i = 0; i < files.Count; i++)
                Console.WriteLine($"{i} - {Path.GetFileName(files[i])}");

            Console.WriteLine();
            Console.Write("Select file number to open : ");
            string choice = Console.ReadLine();

            if (int.TryParse(choice, out int sel) && sel >= 0 && sel < files.Count)
            {
                fileName = files[sel];
                if (!fileName.StartsWith(@"0:\")) { fileName = @"0:\" + fileName; }

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
                    Console.WriteLine($"Error while reading : {e.Message}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey(true);
                }
            }

            forceRefresh = true;
        }

        private void Save()
        {
            Console.Clear();
            if (string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine("File name :");
                Console.Write("> ");
                do
                {
                    fileName = Console.ReadLine();
                } while (string.IsNullOrEmpty(fileName));
            }

            if (!fileName.EndsWith(".txt"))
                fileName += ".txt";
            if (!fileName.StartsWith(@"0:\"))
                fileName = $@"0:\{fileName}";

            try
            {
                File.WriteAllLines(fileName, lines);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"File saved : {fileName}");
                isSaved = true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while saving : {e.Message}");
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press any key to continue...");
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
