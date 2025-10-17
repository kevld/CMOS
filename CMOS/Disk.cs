using System;
using Sys = Cosmos.System;

namespace CMOS
{
    public class Disk : IApp
    {
        public void About()
        {
            Console.WriteLine("Disk v0.0.1");
        }

        public void Exit()
        {
            
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
            Sys.FileSystem.CosmosVFS fs = new();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            string label = fs.GetFileSystemLabel(@"0:\");
            string partitionType = fs.GetFileSystemType(@"0:\");
            var freeSpace = fs.GetAvailableFreeSpace(@"0:\") / 1024 / 1024;

            string formattedSpace;

            if (freeSpace >= 1024)
            {
                double availableSpaceGB = freeSpace / 1024.0;
                formattedSpace = $"{availableSpaceGB:F2} Gb";
            }
            else
            {
                formattedSpace = $"{freeSpace:F0} Mb";
            }


            Console.WriteLine($"{label} | {partitionType} | Free Space: {formattedSpace}");
        }
    }
}
