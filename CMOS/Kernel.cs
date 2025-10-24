using CMOS.Framework.Implementation;
using CMOS.Framework.Interface;
using System;
using Sys = Cosmos.System;

namespace CMOS
{
    public class Kernel : Sys.Kernel
    {
        private Shell _shell;

        protected override void BeforeRun()
        {

            Sys.FileSystem.CosmosVFS fs = new();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            fs.Initialize(true);

            IDiskProperties _diskProperties = new CmosDiskProperties(fs);

            _shell = new(_diskProperties);

            SplashScreen.Show();
        }

        protected override void Run()
        {
            _shell.Run();
        }
    }
}
