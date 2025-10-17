using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace CMOS
{
    public class Kernel : Sys.Kernel
    {
        private Shell _shell;

        protected override void BeforeRun()
        {
            Sys.FileSystem.CosmosVFS fs = new Cosmos.System.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);

            _shell = new();

            Console.Clear();
            Console.WriteLine("=== CMOS 0.0.1 ===");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            
        }

        protected override void Run()
        {
            _shell.Run();
        }
    }
}
