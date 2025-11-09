using CMOS.Framework.Interface;
using System;
using System.IO;
using Sys = Cosmos.System;

namespace CMOS
{
    public class CmosDiskProperties : IDiskProperties
    {
        private const string ROOT = @"0:\";

        private readonly Sys.FileSystem.CosmosVFS _fs;

        public CmosDiskProperties(Sys.FileSystem.CosmosVFS fs)
        {
            if(fs == null) throw new ArgumentNullException(nameof(fs));
           
            _fs = fs;
        }

        public string Label => _fs.GetFileSystemLabel(ROOT);

        public string PartitionType => _fs.GetFileSystemType(ROOT);

        public long FreeSpace => _fs.GetAvailableFreeSpace(ROOT);

        public string RootPath => ROOT;

        public void CreateFile(string path, string content)
        {

            try
            {
                File.WriteAllText(path, content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
