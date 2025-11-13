using CMOS.Common.Extensions;
using CMOS.Framework.Abstract;
using CMOS.Framework.Interface;
using CMOS.Ressources;

namespace CMOS.Apps
{
    public class Disk : App
    {
        private readonly IDiskProperties _diskProperties;

        public Disk(IDiskProperties diskProperties) : base(Version.Major, Version.Minor, Version.Build)
        {
            if(diskProperties == null) throw new ArgumentNullException(nameof(diskProperties));
            _diskProperties = diskProperties;
        }

        public override void About()
        {
            Console.WriteLine($"Disk {GetVersion()}");
        }

        public override void Exit()
        {
            
        }

        public override void Help()
        {
        }

        public override void Run()
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

            Console.WriteLine($"{_diskProperties.Label} | {_diskProperties.PartitionType} | {Translation.APP_DISK_LABEL_FREE_SPACE.Translate()}: {formattedSpace}");
        }
    }
}
