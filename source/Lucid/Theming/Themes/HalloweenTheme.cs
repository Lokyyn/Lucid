using System.Drawing;

namespace Lucid.Theming.Themes
{
    public class HalloweenTheme : BaseThemes.BaseDarkTheme
    {
        public HalloweenTheme() 
            : base()
        {
            Enabled = false;
            MultilanguageKey = "Analyst.Settings.Theme.Halloween";
            ImageKey = "halloween_theme";
            ThemeName = "HalloweenDark";
            OrderNo = 20;

            Colors.DockActive = Color.FromArgb(255, 132, 0);
            Colors.MainAccent = Color.FromArgb(251, 140, 0);

            Colors.ControlHighlight = Color.FromArgb(251, 140, 0);

            Colors.BlueBackground = Color.FromArgb(251, 140, 0);
            Colors.DarkBlueBackground = Color.FromArgb(239, 108, 0);

            Colors.DockMovedHighlight = Color.FromArgb(251, 140, 0);

            Colors.LabelLinkAccent = Color.FromArgb(255, 143, 0);
            Colors.LabelLinkHoveredAccent = Color.FromArgb(255, 87, 34);
        }
    }
}
