namespace Lucid.Theming.Themes.BaseThemes;

/// <summary>
/// Functions as the base for all themes that are based on light colors.
/// </summary>
public abstract class BaseLightTheme : ITheme
{
    public Sizes Sizes { get; }

    public Colors Colors { get; }

    public ThemeType Type => ThemeType.Light;

    public bool Enabled { get; set; }

    public string MultilanguageKey { get; set; }

    public string ImageKey { get; set; }

    public string ThemeName { get; set; }

    public int OrderNo { get; set; }

    public BaseLightTheme()
    {
        Colors = new Colors();
        Sizes = new Sizes();

        // ── Backgrounds ───────────────────────────────────────────────────────
        Colors.BackgroundPrimary   = ColorTranslator.FromHtml("#e8e8e8"); // deepest shell / sidebar
        Colors.BackgroundSecondary = ColorTranslator.FromHtml("#f5f5f5"); // standard form / panel bg
        Colors.BackgroundTertiary  = ColorTranslator.FromHtml("#ffffff"); // elevated surface / alt rows

        // ── Surface / Interaction States ──────────────────────────────────────
        Colors.SurfaceDefault      = ColorTranslator.FromHtml("#d8d8d8"); // hover, inactive dock, scrollbar track
        Colors.SurfaceHighlight    = ColorTranslator.FromHtml("#77b2e8"); // selection, active thumb, pressed

        // ── Borders ───────────────────────────────────────────────────────────
        Colors.BorderDefault       = ColorTranslator.FromHtml("#d0d0d0"); // panel edges, separators
        Colors.BorderAccent        = ColorTranslator.FromHtml("#0078d4"); // focused / active control outline

        // ── Accent ────────────────────────────────────────────────────────────
        Colors.Accent              = ColorTranslator.FromHtml("#0078d4"); // selections, links, dock highlight
        Colors.AccentSecondary     = ColorTranslator.FromHtml("#005ba1"); // hovered links, secondary badge

        // ── Text ──────────────────────────────────────────────────────────────
        Colors.TextPrimary         = ColorTranslator.FromHtml("#1e1e1e"); // all readable content
        Colors.TextDisabled        = ColorTranslator.FromHtml("#696969"); // disabled / hint text (WCAG AA on white)

        // ── Sizes (unchanged) ─────────────────────────────────────────────────
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
