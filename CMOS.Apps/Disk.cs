using CMOS.Framework.Interface;

namespace CMOS.Apps
{
    public class Disk : IApp
    {
        private readonly IDiskProperties _diskProperties;

        public Disk(IDiskProperties diskProperties)
        {
            if(diskProperties == null) throw new ArgumentNullException(nameof(diskProperties));
            _diskProperties = diskProperties;
        }

        public void About()
        {
            Console.WriteLine("Disk v0.0.2");
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
            long freeSpace = _diskProperties.FreeSpace / 1024 / 1024;

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

            Console.WriteLine($"{_diskProperties.Label} | {_diskProperties.PartitionType} | Free Space: {formattedSpace}");
        }
    }
}
