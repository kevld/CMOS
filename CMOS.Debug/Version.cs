using System;
using System.Reflection;

namespace CMOS
{
    internal static class Version
    {
        private static readonly System.Version AssemblyVersion =
            Assembly.GetExecutingAssembly().GetName().Version ?? new System.Version(1, 0, 0);

        internal static int Major => AssemblyVersion.Major;

        internal static int Minor => AssemblyVersion.Minor;

        internal static int Build => AssemblyVersion.Build;

        internal static new string ToString()
        {
            return "v" + AssemblyVersion.ToString();
        }
    }
}
