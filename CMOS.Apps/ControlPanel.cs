using CMOS.Common.Extensions;
using CMOS.Framework.Abstract;
using CMOS.Framework.Interface;
using CMOS.Ressources;

namespace CMOS.Apps
{
    public class ControlPanel : App
    {
        private readonly IDiskProperties _diskProperties;
        private const string LANG_FILE = "lang.cfg";
        private const string DEFAULT_LANGUAGE = "en";
        private readonly string _langFileName;

        public string Language { get; private set; }

        public ControlPanel(IDiskProperties diskProperties) : base (Version.Major, Version.Minor, Version.Build)
        {
            _diskProperties = diskProperties;
            _langFileName = _diskProperties.RootPath + LANG_FILE;
            if (File.Exists(_langFileName))
            {
                Language = File.ReadAllText(_langFileName);
            }

            if (string.IsNullOrEmpty(Language))
            {
                Language = DEFAULT_LANGUAGE;
            }
        }

        public override void About()
        {
            Console.WriteLine($"Control panel {GetVersion()}");
        }

        public override void Exit()
        {
        }

        public override void Help()
        {
        }

        public override void Run()
        {
            Console.WriteLine(Translation.APP_CTL_LABEL_SELECT_LANG.Translate());

            string? choice = "";
            do
            {
                Console.Write("> ");
                choice = Console.ReadLine();

            } while (choice != "en" && choice != "fr");

            Language = choice;

            File.WriteAllText(_langFileName, Language);
            Console.WriteLine(Translation.APP_CTL_CONFIG_SAVED.Translate());
        }
    }
}
