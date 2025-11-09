using CMOS.Framework.Interface;

namespace CMOS.Debug
{
    public class DebugDiskProperties : IDiskProperties
    {
        public string Label => throw new NotImplementedException();

        public string PartitionType => throw new NotImplementedException();

        public long FreeSpace => throw new NotImplementedException();

        public string RootPath => @"C:\";

        public void CreateFile(string path, string content)
        {
            throw new NotImplementedException();
        }
    }
}
