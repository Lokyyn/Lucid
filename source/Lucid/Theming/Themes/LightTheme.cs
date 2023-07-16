using System.Drawing;

namespace Lucid.Theming.Themes
{
    public class LightTheme : BaseThemes.BaseLightTheme
    {
        public LightTheme()
            : base()
        {
            Enabled = true;
            MultilanguageKey = "Analyst.Settings.Theme.Light";
            ImageKey = "light_theme";
            ThemeName = "Light";
            OrderNo = 2;
        }
    }
}
