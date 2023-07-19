using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Lucid.Localization
{
    public class Localizer
    {
        private static ResourceManager rm;

        static Localizer()
        {
            rm = new ResourceManager("Lucid.Localization.Resources.Strings", Assembly.GetExecutingAssembly());
        }

        public static string GetString(string key)
        {
            return rm.GetString(key);
        }

        public static string GetString(string key, CultureInfo culture)
        {
            if (culture == null)
                return GetString(key);

            return rm.GetString(key, culture);
        }
    }
}
