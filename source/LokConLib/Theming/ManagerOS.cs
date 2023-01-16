using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace LCL.Theming
{
    internal static class ManagerOS
    {
        internal static CurrentOS WindowsVersion
        {
            get
            {
                var ver = Environment.OSVersion.Version.Build;

                if (ver < 19000)
                    return CurrentOS.OlderThanWindows10;
                if (ver >= 19000 && ver < 22000)
                    return CurrentOS.Windows10;
                else if (ver >= 22000)
                    return CurrentOS.Windows11;
                else
                    return CurrentOS.NewerThanWindows11;
            }
        }

        internal static bool IsWindows10OrWindows11()
        {
            return WindowsVersion == CurrentOS.Windows10 || WindowsVersion == CurrentOS.Windows11;
        }

        private static object RegistryValue(string keyName, string valueName, object defaultValue)
        {
            try
            {
                return Registry.GetValue(keyName, valueName, defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets the current accent color if the OS Version is Windows 10 or Windows 11
        /// </summary>
        /// <returns></returns>
        internal static Color GetAccentColor()
        {
            if (WindowsVersion == CurrentOS.Windows10 || WindowsVersion == CurrentOS.Windows11)
            {
                int accentColorDWord = (int)RegistryValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\DWM", "AccentColor", 0);
                return ParseDWordColor(accentColorDWord);
            }

            return Color.Empty;
        }

        #region Private methods

        private static Color ParseDWordColor(int dword)
        {
            return Color.FromArgb((dword >> 24) & 0xFF, (dword >> 0) & 0xFF, (dword >> 8) & 0xFF, (dword >> 16) & 0xFF);
        }

        #endregion

        internal enum CurrentOS
        {
            OlderThanWindows10,
            Windows10,
            Windows11,
            NewerThanWindows11
        }
    }
}
