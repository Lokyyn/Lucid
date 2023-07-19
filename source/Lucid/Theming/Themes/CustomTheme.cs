namespace Lucid.Theming.Themes;

public class CustomTheme : ITheme
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

       Colors.MainBackgroundColor = ColorTranslator.FromHtml("#3c3f41");        // Standard Background Color for Controls

        Colors.DockInactive = ColorTranslator.FromHtml("#02f47f");
        Colors.DockActive = ColorTranslator.FromHtml("#0277f4");

        // Dock
        Colors.DockBackground = Color.FromArgb(102, 106, 108);      // Color for Dock-Background
        Colors.HeaderBackground = Color.FromArgb(186, 104, 173);    // Color for Dock-Header (inactiv)
        Colors.BlueBackground = Color.FromArgb(186, 104, 104);      // Color for Dock-Header
        Colors.DockMovedHighlight = ColorTranslator.FromHtml("#611cce");
        // Dock


        // List
        Colors.RowEven = ColorTranslator.FromHtml("#e2330f"); // red
        Colors.RowOdd = ColorTranslator.FromHtml("#1a13dd"); // blue

        // List

        Colors.DarkBlueBackground = Color.FromArgb(52, 57, 66);
        Colors.DarkBackground = Color.FromArgb(43, 43, 43);
        Colors.MediumBackground = Color.FromArgb(49, 51, 53);
        Colors.LightBackground = Color.FromArgb(69, 73, 74);
        Colors.LighterBackground = Color.FromArgb(95, 101, 102);
        Colors.LightestBackground = Color.FromArgb(178, 178, 178);
        Colors.LightBorder = Color.FromArgb(81, 81, 81);
        Colors.DarkBorder = Color.FromArgb(51, 51, 51);
        Colors.LightText = Color.FromArgb(220, 220, 220);
        Colors.DisabledText = Color.FromArgb(153, 153, 153);
        Colors.ControlHighlight = Color.FromArgb(104, 151, 187);
        Colors.MainAccent = Color.FromArgb(186, 104, 104);        // Dock Move Highlight Color
        Colors.GreyHighlight = Color.FromArgb(122, 128, 132);
        Colors.GreySelection = Color.FromArgb(92, 92, 92);
        Colors.DarkGreySelection = Color.FromArgb(82, 82, 82);
        Colors.DarkBlueBorder = Color.FromArgb(51, 61, 78);
        Colors.LightBlueBorder = Color.FromArgb(86, 97, 114);
        Colors.ActiveControl = Color.FromArgb(159, 178, 196);   

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
