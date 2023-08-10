namespace Lucid.Theming.Themes;

internal class AnnaLenaTheme : BaseThemes.BaseDarkTheme
{
    public AnnaLenaTheme() 
        : base()
    {
        Enabled = false;
        MultilanguageKey = "";
        ImageKey = "dark_theme";
        ThemeName = "";
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
