namespace Lucid.Theming.Themes;

public class CustomAccentDarkTheme : BaseThemes.BaseDarkTheme
{
    public CustomAccentDarkTheme()
        : base()
    {
        Enabled = false;
        MultilanguageKey = "Analyst.Settings.Theme.AccentDark";
        ImageKey = "dark_theme";
        ThemeName = "AccentDark";
        OrderNo = 10;
    }
}

public class CustomAccentLightTheme : BaseThemes.BaseLightTheme
{
    public CustomAccentLightTheme()
        : base()
    {
        Enabled = false;
        MultilanguageKey = "Analyst.Settings.Theme.AccentLight";
        ImageKey = "light_theme";
        ThemeName = "AccentLight";
        OrderNo = 10;
    }
}
