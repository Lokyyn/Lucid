namespace Lucid.Theming.Themes.BaseThemes;

/// <summary>
/// Functions as the base for all themes that are based on light colors.
/// </summary>
public abstract class BaseLightTheme : ITheme
{
    public Sizes Sizes { get; }

    public Colors Colors { get; set; }

    public ThemeType Type => ThemeType.Light;

    public bool Enabled { get; set; }

    public string MultilanguageKey { get; set; }

    public string ImageKey { get; set; }

    public string ThemeName { get; set; }

    public int OrderNo { get; set; }

    public BaseLightTheme()
    {
        Colors = new Colors
        {
            // Backgrounds
            BackgroundPrimary   = ColorTranslator.FromHtml("#e8e8e8"), // deepest shell / sidebar
            BackgroundSecondary = ColorTranslator.FromHtml("#f5f5f5"), // standard form / panel bg
            BackgroundTertiary  = ColorTranslator.FromHtml("#ffffff"), // elevated surface / alt rows

            // Surface / Interaction States
            SurfaceDefault      = ColorTranslator.FromHtml("#d8d8d8"), // hover, inactive dock, scrollbar track
            SurfaceHighlight    = ColorTranslator.FromHtml("#77b2e8"), // selection, active thumb, pressed

            // Borders
            BorderDefault       = ColorTranslator.FromHtml("#d0d0d0"), // panel edges, separators
            BorderAccent        = ColorTranslator.FromHtml("#0078d4"), // focused / active control outline

            // Accent
            Accent              = ColorTranslator.FromHtml("#0078d4"), // selections, links, dock highlight
            AccentSecondary     = ColorTranslator.FromHtml("#005ba1"), // hovered links, secondary badge

            // Text
            TextPrimary         = ColorTranslator.FromHtml("#1e1e1e"), // all readable content
            TextDisabled        = ColorTranslator.FromHtml("#696969"), // disabled / hint text
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
