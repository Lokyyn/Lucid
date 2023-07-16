using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.Theming.Themes
{
    public class AnnaLenaTheme : BaseThemes.BaseDarkTheme
    {
        public AnnaLenaTheme() 
            : base()
        {
            Enabled = true;
            MultilanguageKey = "Analyst.Settings.Theme.AnnaLenaTheme";
            ImageKey = "dark_theme";
            ThemeName = "AnnaLenaTheme";
            OrderNo = 3;

            // old cyan color that was to bright: #4db6ac

            Colors.DockActive = ColorTranslator.FromHtml("#007069");
            Colors.DockMovedHighlight = ColorTranslator.FromHtml("#44a39a");
            Colors.MainAccent = ColorTranslator.FromHtml("#44a39a");
            Colors.DockMovedHighlight = ColorTranslator.FromHtml("#44a39a");
            Colors.ControlHighlight = ColorTranslator.FromHtml("#44a39a");
            Colors.BlueBackground = ColorTranslator.FromHtml("#007069");
            Colors.LightBlueBorder = ColorTranslator.FromHtml("#378982"); 
            Colors.DarkBlueBorder = ColorTranslator.FromHtml("#003C39");
        }
    }
}
