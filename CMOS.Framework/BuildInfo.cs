using CMOS.Framework.Interface;
using System;
using System.Reflection;

namespace CMOS.Framework
{
    public sealed class BuildInfo : IBuildInfo
    {
        private readonly string _version;
        private readonly int _major;
        private readonly int _minor;
        private readonly int _build;

        public BuildInfo(int major, int minor, int build)
        {
            _major = major;
            _minor = minor;
            _build = build;
            _version = PrepareVersion();
        }

        public BuildInfo(string version)
        {
            _version = version;

            string[] splittedVersion = version.Split('.');
            _major = int.Parse(splittedVersion[0]);
            _minor = int.Parse(splittedVersion[1]);
            _build = int.Parse(splittedVersion[2]);
        }

        private string PrepareVersion()
        {
            return _major + "." + _minor + "." + _build;
        }

        public int Major => _major;

        public int Minor => _minor;

        public int Build => _build;

        public new string ToString()
        {
            return "v" + _version;
        }
    }
}
