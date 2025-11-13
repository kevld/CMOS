
using CMOS.Ressources;

namespace CMOS.Common.Extensions
{
    public static class StringExtensions
    {
        public static string Translate(this string key)
        {
            if (!string.IsNullOrEmpty(key))
            {

                if (Container.Instance.Translation.TryGetValue(key, out string? value))
                {
                    return value;
                }

                Dictionary<string, string> defautTranslation = Translation.LoadEn();
                if (defautTranslation.TryGetValue(key, out string? defaultValue))
                {
                    return defaultValue;
                }
            }

            return $"(?{key}?)";
        }
    }
}
