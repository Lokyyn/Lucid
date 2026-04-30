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

       Colors.BackgroundSecondary = ColorTranslator.FromHtml("#3c3f41");        // Standard Background Color for Controls

        Colors.SurfaceDefault = ColorTranslator.FromHtml("#02f47f");
        Colors.DockActive = ColorTranslator.FromHtml("#0277f4");

        // Dock
        Colors.BackgroundTertiary = Color.FromArgb(102, 106, 108);      // Color for Dock-Background
        Colors.BackgroundSecondary = Color.FromArgb(186, 104, 173);    // Color for Dock-Header (inactiv)
        Colors.Accent = Color.FromArgb(186, 104, 104);      // Color for Dock-Header
        Colors.Accent = ColorTranslator.FromHtml("#611cce");
        // Dock


        // List
        Colors.BackgroundTertiary = ColorTranslator.FromHtml("#e2330f"); // red
        Colors.BackgroundSecondary = ColorTranslator.FromHtml("#1a13dd"); // blue

        // List

        Colors.DarkBlueBackground = Color.FromArgb(52, 57, 66);
        Colors.BackgroundPrimary = Color.FromArgb(43, 43, 43);
        Colors.BackgroundPrimary = Color.FromArgb(49, 51, 53);
        Colors.BackgroundTertiary = Color.FromArgb(69, 73, 74);
        Colors.SurfaceDefault = Color.FromArgb(95, 101, 102);
        Colors.SurfaceHighlight = Color.FromArgb(178, 178, 178);
        Colors.BorderDefault = Color.FromArgb(81, 81, 81);
        Colors.BorderDefault = Color.FromArgb(51, 51, 51);
        Colors.LightText = Color.FromArgb(220, 220, 220);
        Colors.DisabledText = Color.FromArgb(153, 153, 153);
        Colors.ControlHighlight = Color.FromArgb(104, 151, 187);
        Colors.Accent = Color.FromArgb(186, 104, 104);        // Dock Move Highlight Color
        Colors.SurfaceDefault = Color.FromArgb(122, 128, 132);
        Colors.SurfaceHighlight = Color.FromArgb(92, 92, 92);
        Colors.SurfaceHighlight = Color.FromArgb(82, 82, 82);
        Colors.BorderAccent = Color.FromArgb(51, 61, 78);
        Colors.BorderAccent = Color.FromArgb(86, 97, 114);
        Colors.SurfaceHighlight = Color.FromArgb(159, 178, 196);   

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
