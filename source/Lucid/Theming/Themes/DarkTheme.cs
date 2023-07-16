using System.Drawing;

namespace Lucid.Theming.Themes
{
    public class DarkTheme : BaseThemes.BaseDarkTheme
    {
        public DarkTheme() 
            : base()
        {
            Enabled = true;
            MultilanguageKey = "Analyst.Settings.Theme.Dark";
            ImageKey = "dark_theme";
            ThemeName = "Dark";
            OrderNo = 1;
        }
    }
}
