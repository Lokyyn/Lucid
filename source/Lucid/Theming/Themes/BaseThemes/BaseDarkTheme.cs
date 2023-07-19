namespace Lucid.Theming.Themes.BaseThemes
{
    public abstract class BaseDarkTheme : ITheme
    {
        public Sizes Sizes { get; }

        public Colors Colors { get; }

        public ThemeType Type => ThemeType.Dark;

        public bool Enabled { get; internal set; }

        public string MultilanguageKey { get; internal set; }

        public string ImageKey { get; internal set; }

        public string ThemeName { get; internal set; }

        public int OrderNo { get; internal set; }

        public BaseDarkTheme()
        {
            Colors = new Colors();
            Sizes = new Sizes();

            Colors.DockBackground = ColorTranslator.FromHtml("#464646");
            Colors.RowEven = ColorTranslator.FromHtml("#444749");
            Colors.RowOdd = ColorTranslator.FromHtml("#3c3f41");
            Colors.DockMovedHighlight = ColorTranslator.FromHtml("#4b6eaf");
            Colors.DockActive = ColorTranslator.FromHtml("#375182");
            Colors.DockInactive = ColorTranslator.FromHtml("#393c3e");

            Colors.MainBackgroundColor = ColorTranslator.FromHtml("#3c3f41");
            Colors.HeaderBackground = ColorTranslator.FromHtml("#393c3e");
            Colors.BlueBackground = ColorTranslator.FromHtml("#375182");
            Colors.DarkBlueBackground = ColorTranslator.FromHtml("#343942");
            Colors.DarkBackground = ColorTranslator.FromHtml("#2b2b2b");
            Colors.MediumBackground = ColorTranslator.FromHtml("#313335");
            Colors.LightBackground = ColorTranslator.FromHtml("#45494a");
            Colors.LighterBackground = ColorTranslator.FromHtml("#5f6566");
            Colors.LightestBackground = ColorTranslator.FromHtml("#b2b2b2");
            Colors.LightBorder = ColorTranslator.FromHtml("#515151");
            Colors.DarkBorder = ColorTranslator.FromHtml("#333333");
            Colors.LightText = ColorTranslator.FromHtml("#dcdcdc");
            Colors.DisabledText = ColorTranslator.FromHtml("#999999");
            Colors.ControlHighlight = ColorTranslator.FromHtml("#6897bb");
            Colors.MainAccent = ColorTranslator.FromHtml("#4b6eaf");
            Colors.GreyHighlight = ColorTranslator.FromHtml("#4e5457");
            Colors.GreySelection = ColorTranslator.FromHtml("#5c5c5c");
            Colors.DarkGreySelection = ColorTranslator.FromHtml("#525252");
            Colors.DarkBlueBorder = ColorTranslator.FromHtml("#333d4e");
            Colors.LightBlueBorder = ColorTranslator.FromHtml("#566172");
            Colors.ActiveControl = ColorTranslator.FromHtml("#768287");
            Colors.LabelLinkAccent = ColorTranslator.FromHtml("#4F73FB");
            Colors.LabelLinkHoveredAccent = ColorTranslator.FromHtml("#8ba1ff");
            Colors.InactivScrollbar = ColorTranslator.FromHtml("#4e5457");
            Colors.HotScrollbar = ColorTranslator.FromHtml("#60686d");

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
}
