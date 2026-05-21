namespace Lucid.Theming.Themes.BaseThemes;

/// <summary>
/// Functions as the base for all themes that are based on dark colors
/// </summary>
public abstract class BaseDarkTheme : ITheme
{
    public Sizes Sizes { get; }

    public Colors Colors { get; }

    public ThemeType Type => ThemeType.Dark;

    public bool Enabled { get; set; }

    public string MultilanguageKey { get; set; }

    public string ImageKey { get; set; }

    public string ThemeName { get; set; }

    public int OrderNo { get; set; }

    public BaseDarkTheme()
    {
        Colors = new Colors();
        Sizes = new Sizes();

        Colors.BackgroundPrimary   = ColorTranslator.FromHtml("#2b2b2b");
        Colors.BackgroundSecondary = ColorTranslator.FromHtml("#3c3f41");
        Colors.BackgroundTertiary  = ColorTranslator.FromHtml("#464646");

        Colors.SurfaceDefault      = ColorTranslator.FromHtml("#4e5457");
        Colors.SurfaceHighlight    = ColorTranslator.FromHtml("#5c5c5c");

        Colors.BorderDefault       = ColorTranslator.FromHtml("#515151");
        Colors.BorderAccent        = ColorTranslator.FromHtml("#566172");

        Colors.Accent              = ColorTranslator.FromHtml("#4b6eaf");
        Colors.AccentSecondary     = ColorTranslator.FromHtml("#343942");

        Colors.TextPrimary         = ColorTranslator.FromHtml("#dcdcdc");
        Colors.TextDisabled        = ColorTranslator.FromHtml("#999999");

        Sizes.Padding = 10;
        Sizes.ScrollBarSize = 15;
        Sizes.ArrowButtonSize = 15;
        Sizes.MinimumThumbSize = 11;
        Sizes.CheckBoxSize = 12;
        Sizes.RadioButtonSize = 12;
        Sizes.ToolWindowHeaderSize = 25;
        Sizes.DocumentTabAreaSize = 24;
        Sizes.ToolWindowTabAreaSize = 21;
    }
}
