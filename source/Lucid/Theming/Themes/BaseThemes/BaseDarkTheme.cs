namespace Lucid.Theming.Themes.BaseThemes;

/// <summary>
/// Functions as the base for all themes that are based on dark colors.
/// </summary>
public abstract class BaseDarkTheme : ITheme
{
    public Sizes Sizes { get; }

    public Colors Colors { get; set; }

    public ThemeType Type => ThemeType.Dark;

    public bool Enabled { get; set; }

    public string MultilanguageKey { get; set; }

    public string ImageKey { get; set; }

    public string ThemeName { get; set; }

    public int OrderNo { get; set; }

    public BaseDarkTheme()
    {
        Colors = new Colors
        {
            // Backgrounds
            BackgroundPrimary   = ColorTranslator.FromHtml("#1e1f22"), // deepest shell / sidebar
            BackgroundSecondary = ColorTranslator.FromHtml("#2b2d30"), // standard form / panel bg
            BackgroundTertiary  = ColorTranslator.FromHtml("#393b40"), // elevated surface / alt rows

            // Surface / Interaction States
            SurfaceDefault      = ColorTranslator.FromHtml("#43454a"), // hover, inactive dock, scrollbar track
            SurfaceHighlight    = ColorTranslator.FromHtml("#2d4f7c"), // selection, active thumb, pressed

            // Borders
            BorderDefault       = ColorTranslator.FromHtml("#4a4d52"), // panel edges, separators
            BorderAccent        = ColorTranslator.FromHtml("#4d8fd9"), // focused / active control outline

            // Accent
            Accent              = ColorTranslator.FromHtml("#4b6eaf"), // selections, links, dock highlight
            AccentSecondary     = ColorTranslator.FromHtml("#343942"), // hovered links, secondary badge

            // Text
            TextPrimary         = ColorTranslator.FromHtml("#dde1e7"), // all readable content
            TextDisabled        = ColorTranslator.FromHtml("#9da3b4"), // disabled / hint text
        };

        Sizes = new Sizes();
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
