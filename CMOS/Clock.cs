
using System;

namespace CMOS
{
    public class Clock : IApp
    {
        private bool _isRunning = true;

        public void About()
        {
            Console.WriteLine("Clock v0.0.1");
        }

        public void Exit()
        {
            _isRunning = false;
        }

        public void Help()
        {
            Console.WriteLine("About / a     : About");
            Console.WriteLine("Exit / e      : Exit");
            Console.WriteLine("Help / ?      : Display this menu");
            Console.WriteLine();
        }

        public void Run()
        {

            while (_isRunning)
            {
                Console.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                System.Threading.Thread.Sleep(1000);


                ClearLastLine();
                ManageInput();
            }
        }

        private void ClearLastLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write("                               ");
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        private void ManageInput()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.E)
                {
                    Exit();
                }
                if(key.Key == ConsoleKey.A)
                {
                    About();
                }
                if (key.Key == ConsoleKey.H)
                {
                    Help();
                }
            }
        }
    }
}
