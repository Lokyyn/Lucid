namespace Lucid.Theming.Themes.BaseThemes;

/// <summary>
/// Functions as the base for all themes that are based on light colors
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

        Colors.BackgroundPrimary   = ColorTranslator.FromHtml("#a0a0a0");
        Colors.BackgroundSecondary = ColorTranslator.FromHtml("#b4b7b9");
        Colors.BackgroundTertiary  = ColorTranslator.FromHtml("#c7cbcd");

        Colors.SurfaceDefault      = ColorTranslator.FromHtml("#919596");
        Colors.SurfaceHighlight    = ColorTranslator.FromHtml("#a8aeb0");

        Colors.BorderDefault       = ColorTranslator.FromHtml("#c9c9c9");
        Colors.BorderAccent        = ColorTranslator.FromHtml("#5098dd");

        Colors.Accent              = ColorTranslator.FromHtml("#0082d6");
        Colors.AccentSecondary     = ColorTranslator.FromHtml("#5d7bdb");

        Colors.TextPrimary         = ColorTranslator.FromHtml("#141414");
        Colors.TextDisabled        = ColorTranslator.FromHtml("#676767");

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
