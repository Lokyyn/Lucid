namespace Lucid.Theming.Themes;

/// <summary>
/// Dark theme with green accent colors.
/// </summary>
public class DarkGreenTheme : BaseThemes.BaseDarkTheme
{
    public DarkGreenTheme()
        : base()
    {
        Enabled = true;
        MultilanguageKey = "";
        ImageKey = "dark_theme";
        ThemeName = "Dark Green";
        OrderNo = 3;

        Colors.Accent              = ColorTranslator.FromHtml("#56a764");
        Colors.AccentSecondary     = ColorTranslator.FromHtml("#1e4a28");
        Colors.BorderAccent        = ColorTranslator.FromHtml("#56a764");
        Colors.SurfaceHighlight    = ColorTranslator.FromHtml("#1e3d28");
    }
}

/// <summary>
/// Dark theme with purple accent colors.
/// </summary>
public class DarkPurpleTheme : BaseThemes.BaseDarkTheme
{
    public DarkPurpleTheme()
        : base()
    {
        Enabled = true;
        MultilanguageKey = "";
        ImageKey = "dark_theme";
        ThemeName = "Dark Purple";
        OrderNo = 4;

        Colors.Accent              = ColorTranslator.FromHtml("#9070c8");
        Colors.AccentSecondary     = ColorTranslator.FromHtml("#3c2068");
        Colors.BorderAccent        = ColorTranslator.FromHtml("#9070c8");
        Colors.SurfaceHighlight    = ColorTranslator.FromHtml("#2a1850");
    }
}

/// <summary>
/// Light theme with teal accent colors.
/// </summary>
public class LightTealTheme : BaseThemes.BaseLightTheme
{
    public LightTealTheme()
        : base()
    {
        Enabled = true;
        MultilanguageKey = "";
        ImageKey = "light_theme";
        ThemeName = "Light Teal";
        OrderNo = 5;

        Colors.Accent              = ColorTranslator.FromHtml("#007b8a");
        Colors.AccentSecondary     = ColorTranslator.FromHtml("#005562");
        Colors.BorderAccent        = ColorTranslator.FromHtml("#007b8a");
        Colors.SurfaceHighlight    = ColorTranslator.FromHtml("#b3e5ed");
    }
}
