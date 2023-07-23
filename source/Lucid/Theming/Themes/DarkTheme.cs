namespace Lucid.Theming.Themes;

/// <summary>
/// Standard Theme for dark colors
/// </summary>
public class DarkTheme : BaseThemes.BaseDarkTheme
{
    public DarkTheme() 
        : base()
    {
        Enabled = true;
        MultilanguageKey = "";
        ImageKey = "dark_theme";
        ThemeName = "Dark";
        OrderNo = 1;
    }
}
