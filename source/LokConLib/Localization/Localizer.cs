using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Localization
{
    public class Localizer
    {
        private static ResourceManager rm;

        static Localizer()
        {
            rm = new ResourceManager("LCL.Localization.Resources.Strings", Assembly.GetExecutingAssembly());
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
