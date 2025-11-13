
using CMOS.Common.Extensions;
using CMOS.Framework.Abstract;
using CMOS.Ressources;

namespace CMOS.Apps
{
    public class Clock : App
    {
        private bool _isRunning = true;

        public Clock() : base(Version.Major, Version.Minor, Version.Build)
        {
            
        }

        public override void About()
        {
            Console.WriteLine($"Clock {GetVersion()}");
        }

        public override void Exit()
        {
            _isRunning = false;
        }

        public override void Help()
        {
            Console.WriteLine($"Alt + a     : {Translation.GENERIC_PROGRAM_HELP_ABOUT.Translate()}");
            Console.WriteLine($"Alt + e     : {Translation.GENERIC_PROGRAM_HELP_EXIT.Translate()}");
            Console.WriteLine($"Alt + H     : {Translation.GENERIC_PROGRAM_HELP_HELP.Translate()}");
            Console.WriteLine();
        }

        public override void Run()
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
                var keyInfo = Console.ReadKey(true);
                if ((keyInfo.Modifiers & ConsoleModifiers.Alt) != 0)
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.E: Exit(); break;
                        case ConsoleKey.A: About(); break;
                        case ConsoleKey.H: Help(); break;
                    }
                }
                    
            }
        }
    }
}
