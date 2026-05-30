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
        Colors.BackgroundPrimary   = ColorTranslator.FromHtml("#1e1f22"); // deepest shell / sidebar
        Colors.BackgroundSecondary = ColorTranslator.FromHtml("#2b2d30"); // standard form / panel bg
        Colors.BackgroundTertiary  = ColorTranslator.FromHtml("#393b40"); // elevated surface / alt rows

        // Surface / Interaction States
        Colors.SurfaceDefault      = ColorTranslator.FromHtml("#43454a"); // hover, inactive dock, scrollbar track
        Colors.SurfaceHighlight    = ColorTranslator.FromHtml("#2d4f7c"); // selection, active thumb, pressed

        // Borders
        Colors.BorderDefault       = ColorTranslator.FromHtml("#4a4d52"); // panel edges, separators
        Colors.BorderAccent        = ColorTranslator.FromHtml("#4d8fd9"); // focused / active control outline

        // Accent
        Colors.Accent              = ColorTranslator.FromHtml("#5b9bd5"); // selections, links, dock highlight
        Colors.AccentSecondary     = ColorTranslator.FromHtml("#1c3c6e"); // hovered links, secondary badge

        // Text
        Colors.TextPrimary         = ColorTranslator.FromHtml("#dde1e7"); // all readable content
        Colors.TextDisabled        = ColorTranslator.FromHtml("#9da3b4"); // disabled / hint text

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
