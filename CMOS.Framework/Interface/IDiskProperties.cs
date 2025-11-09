using System;
namespace CMOS.Framework.Interface
{
    public interface IDiskProperties
    {
        public string Label { get; }

        public string PartitionType { get; }

        public long FreeSpace { get; }

        public string RootPath { get; }

        public void CreateFile(string path, string content);
    }
}
