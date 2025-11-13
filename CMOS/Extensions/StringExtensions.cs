
using CMOS.Common;
using CMOS.Ressources;
using System.Collections.Generic;

namespace CMOS.Extensions
{
    public static class StringExtensions
    {
        public static string Translate(this string key)
        {
            if(Container.Instance.Translation.TryGetValue(key, out string value))
            {
                return value;
            }

            Dictionary<string, string> defautTranslation = Translation.LoadEn();
            if(defautTranslation.TryGetValue(key, out string defaultValue))
            {
                return value;
            }

            return "(?)";
        }
    }
}
