using CMOS.Common;
using CMOS.Framework.Interface;
using CMOS.Managers;
using Sys = Cosmos.System;

namespace CMOS
{
    public class Kernel : Sys.Kernel
    {
        private Shell _shell;

        private const string KERNEL_VERSION = "0.0.4";

        protected override void BeforeRun()
        {
            Sys.FileSystem.CosmosVFS fs = new();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            fs.Initialize(true);

            IDiskProperties _diskProperties = new CmosDiskProperties(fs);

            LanguageManager lm = new LanguageManager(_diskProperties);
            Container.Instance.Language = lm.GetLanguage();
            lm.LoadTranslation();
            Container.Instance.Version = KERNEL_VERSION;

            _shell = new(_diskProperties);
            SplashScreen.Show();
        }

        protected override void Run()
        {
            _shell.Run();
        }
    }
}
