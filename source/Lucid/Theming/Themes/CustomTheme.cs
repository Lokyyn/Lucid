namespace Lucid.Theming.Themes;

internal class CustomTheme : ITheme
{
    public Sizes Sizes { get; } = new Sizes();

    public Colors Colors { get; } = new Colors();

    public ThemeType Type => ThemeType.Dark;

    public bool Enabled => false;

    public string MultilanguageKey => throw new System.NotImplementedException();

    public string ImageKey => throw new System.NotImplementedException();

    public string ThemeName => throw new System.NotImplementedException();

    public int OrderNo => throw new System.NotImplementedException();

    public CustomTheme()
    {
        Colors.BackgroundSecondary = ColorTranslator.FromHtml("#3c3f41");

        Colors.SurfaceDefault   = ColorTranslator.FromHtml("#02f47f");
        Colors.Accent           = ColorTranslator.FromHtml("#0277f4");

        Colors.BackgroundTertiary  = Color.FromArgb(102, 106, 108);
        Colors.AccentSecondary     = Color.FromArgb(186, 104, 173);

        Colors.BackgroundPrimary   = Color.FromArgb(43, 43, 43);
        Colors.BorderDefault       = Color.FromArgb(81, 81, 81);
        Colors.TextPrimary         = Color.FromArgb(220, 220, 220);
        Colors.TextDisabled        = Color.FromArgb(153, 153, 153);
        Colors.SurfaceHighlight    = Color.FromArgb(92, 92, 92);
        Colors.BorderAccent        = Color.FromArgb(86, 97, 114);

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
