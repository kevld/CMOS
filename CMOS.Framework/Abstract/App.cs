using CMOS.Framework.Interface;

namespace CMOS.Framework.Abstract
{
    public abstract class App : IApp
    {
        private readonly int _major;
        private readonly int _minor;
        private readonly int _build;

        public App(int major, int minor, int build)
        {
            _major = major;
            _minor = minor;
            _build = build;
        }

        protected int Major => _major;
        protected int Minor => _minor;
        protected int Build => _build;

        public abstract void About();

        public abstract void Exit();

        public virtual string GetVersion()
        {
            BuildInfo buildInfo = new BuildInfo(Major, Minor, Build);
            return buildInfo.ToString();
        }

        public abstract void Help();

        public abstract void Run();
    }
}
