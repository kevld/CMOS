namespace CMOS.Common
{
    public sealed class Container
    {
        private static readonly Container _instance = new Container();

        private Container()
        {
            Language = "en";
            Version = "0.0.0";
            Translation = new();
        }

        public static Container Instance
        {
            get
            {
                return _instance;
            }
        }

        public string Language { get; set; }

        public string Version { get; set; }

        public Dictionary<string, string> Translation { get; set; }
    }
}
