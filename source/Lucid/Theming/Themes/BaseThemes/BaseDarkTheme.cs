namespace Lucid.Theming.Themes.BaseThemes;

/// <summary>
/// Functions as the base for all themes that are based on dark colors.
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

        // Backgrounds
        Colors.BackgroundPrimary   = ColorTranslator.FromHtml("#2b2b2b"); // deepest shell / sidebar
        Colors.BackgroundSecondary = ColorTranslator.FromHtml("#3c3f41"); // standard form / panel bg
        Colors.BackgroundTertiary  = ColorTranslator.FromHtml("#464646"); // elevated surface / alt rows

        // Surface / Interaction States
        Colors.SurfaceDefault      = ColorTranslator.FromHtml("#4e5457"); // hover, inactive dock, scrollbar track
        Colors.SurfaceHighlight    = ColorTranslator.FromHtml("#5c5c5c"); // selection, active thumb, pressed

        // Borders
        Colors.BorderDefault       = ColorTranslator.FromHtml("#515151"); // panel edges, separators
        Colors.BorderAccent        = ColorTranslator.FromHtml("#566172"); // focused / active control outline

        // Accent
        Colors.Accent              = ColorTranslator.FromHtml("#4b6eaf"); // selections, links, dock highlight
        Colors.AccentSecondary     = ColorTranslator.FromHtml("#343942"); // hovered links, secondary badge

        // Text
        Colors.TextPrimary         = ColorTranslator.FromHtml("#dcdcdc"); // all readable content
        Colors.TextDisabled        = ColorTranslator.FromHtml("#999999"); // disabled / hint text

        // Sizes (unchanged)
        Sizes.Padding              = 10;
        Sizes.ScrollBarSize        = 15;
        Sizes.ArrowButtonSize      = 15;
        Sizes.MinimumThumbSize     = 11;
        Sizes.CheckBoxSize         = 12;
        Sizes.RadioButtonSize      = 12;
        Sizes.ToolWindowHeaderSize = 25;
        Sizes.DocumentTabAreaSize  = 24;
        Sizes.ToolWindowTabAreaSize = 21;
    }
}
