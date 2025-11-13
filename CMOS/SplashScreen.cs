using CMOS.Framework;
using System;
using System.Threading;

namespace CMOS
{
    public class SplashScreen
    {
        private const string VERSION = "0.0.4";

        public static void Show()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();

            // Upper line
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("+------------------------------------+");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("|                                    |");

            // Line 1
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("|  ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" #####  ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("#     # ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" #####  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" #####");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("    |");

            // Line 2
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("|  ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("#     # ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("##   ## ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("#     # ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("#     ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("    |");

            // Line 3
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("|  ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("#       ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("# # # # ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("#     # ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" ##### ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   |");

            // Line 4
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("|  ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("#       ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("#  #  # ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("#     # ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("      #");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   |");

            // Line 5
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("|  ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("#     # ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("#     # ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("#     # ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("#     #");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   |");

            // Line 6
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("|  ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" #####  ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("#     # ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" #####  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" #####");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("    |");

            Console.Write("|                             ");
            WriteVersion();

            // bottom line
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" |");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("+------------------------------------+");
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void WriteVersion()
        {
            BuildInfo buildInfo = new BuildInfo(VERSION);

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("v");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(Version.Major);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(".");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(Version.Minor);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(".");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(Version.Build);
        }
    }
}
