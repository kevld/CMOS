using CMOS.Common;
using CMOS.Framework.Interface;
using CMOS.Ressources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMOS.Managers
{
    public class LanguageManager
    {
        private readonly IDiskProperties _diskProperties;
        private const string LANG_FILE = "lang.cfg";
        private const string DEFAULT_LANGUAGE = "en";
        private readonly string _langFileName;

        public LanguageManager(IDiskProperties diskProperties)
        {
            _diskProperties = diskProperties;
            _langFileName = _diskProperties.RootPath + LANG_FILE;
        }

        public string GetLanguage()
        {
            string language = "";
            if (File.Exists(_langFileName))
            {
                language = File.ReadAllText(_langFileName);
            }

            if (string.IsNullOrEmpty(language))
            {
                language = DEFAULT_LANGUAGE;
            }

            return language;
        }

        public void LoadTranslation()
        {
            string language = GetLanguage();

            if(language == "fr")
            {
                Container.Instance.Translation = Translation.LoadFr();
            }

            else
            {
                Container.Instance.Translation = Translation.LoadEn();
            }
        }
    }
}
