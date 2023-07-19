namespace Lucid.Theming.Themes.BaseThemes;

public abstract class BaseLightTheme : ITheme
{
    public Sizes Sizes { get; }

    public Colors Colors { get; }

    public ThemeType Type => ThemeType.Light;

    public bool Enabled { get; internal set; }

    public string MultilanguageKey { get; internal set; }

    public string ImageKey { get; internal set; }

    public string ThemeName { get; internal set; }

    public int OrderNo { get; internal set; }

    public BaseLightTheme()
    {
        Colors = new Colors();
        Sizes = new Sizes();

        Colors.DockBackground = ColorTranslator.FromHtml("#c7cbcd");
        Colors.RowEven = ColorTranslator.FromHtml("#9da0a3");
        Colors.RowOdd = ColorTranslator.FromHtml("#94989a");
        Colors.DockMovedHighlight = ColorTranslator.FromHtml("#4b6eaf");
        Colors.DockActive = ColorTranslator.FromHtml("#0082d6");
        Colors.DockInactive = ColorTranslator.FromHtml("#c0c4c7");

        Colors.MainBackgroundColor = ColorTranslator.FromHtml("#b4b7b9");
        Colors.HeaderBackground = ColorTranslator.FromHtml("#b1b4b6");
        Colors.BlueBackground = ColorTranslator.FromHtml("#0082d6");
        Colors.DarkBlueBackground = ColorTranslator.FromHtml("#acb1ba");
        Colors.DarkBackground = ColorTranslator.FromHtml("#a0a0a0");
        Colors.MediumBackground = ColorTranslator.FromHtml("#9fa1a3");
        Colors.LightBackground = ColorTranslator.FromHtml("#bdc1c2");
        Colors.LighterBackground = ColorTranslator.FromHtml("#7d7e80");
        Colors.LightestBackground = ColorTranslator.FromHtml("#b2b2b2");
        Colors.LightBorder = ColorTranslator.FromHtml("#c9c9c9");
        Colors.DarkBorder = ColorTranslator.FromHtml("#a3a3a3");
        Colors.LightText = ColorTranslator.FromHtml("#141414");
        Colors.DisabledText = ColorTranslator.FromHtml("#676767");
        Colors.ControlHighlight = ColorTranslator.FromHtml("#6897bb");
        Colors.MainAccent = ColorTranslator.FromHtml("#3b98e3");
        Colors.GreyHighlight = ColorTranslator.FromHtml("#919596"); // Theming Bug fixed for MenuStrip Items
        Colors.GreySelection = ColorTranslator.FromHtml("#a8aeb0");
        Colors.DarkGreySelection = ColorTranslator.FromHtml("#cacaca");
        Colors.DarkBlueBorder = ColorTranslator.FromHtml("#677b8e");
        Colors.LightBlueBorder = ColorTranslator.FromHtml("#5098dd");
        Colors.ActiveControl = ColorTranslator.FromHtml("#606C77");
        Colors.LabelLinkAccent = ColorTranslator.FromHtml("#0048c1");
        Colors.LabelLinkHoveredAccent = ColorTranslator.FromHtml("#5d7bdb");
        Colors.InactivScrollbar = ColorTranslator.FromHtml("#8d9394");
        Colors.HotScrollbar = ColorTranslator.FromHtml("#7a8182");

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
