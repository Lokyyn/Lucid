namespace Lucid.Theming.Themes;

/// <summary>
/// Standard Theme for dark colors
/// </summary>
public class LightTheme : BaseThemes.BaseLightTheme
{
    public LightTheme()
        : base()
    {
        Enabled = true;
        MultilanguageKey = "";
        ImageKey = "light_theme";
        ThemeName = "Light";
        OrderNo = 2;
    }
}
