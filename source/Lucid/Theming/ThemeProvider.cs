using Lucid.Theming.Themes;
using Lucid.Theming.Themes.BaseThemes;

namespace Lucid.Theming;

public class ThemeProvider
{
    private static List<ITheme> _allThemes;
    private static ITheme theme;

    static ThemeProvider()
    {
        _allThemes = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(t => !t.IsInterface && (t.BaseType == typeof(BaseDarkTheme) || t.BaseType == typeof(BaseLightTheme))).Select(c => (ITheme)Activator.CreateInstance(c)).Where(u => u.Enabled).ToList();

        LoadAddtionalThemes();
    }

    /// <summary>
    /// This event fires when the current theme is changed
    /// </summary>
    public static event ThemeChangedHandler OnThemeChanged;

    public delegate void ThemeChangedHandler();

    /// <summary>
    /// The current active theme
    /// </summary>
    public static ITheme Theme
    {
        get
        {
            if (theme == null)
                theme = new DarkTheme();

            return theme;
        }
        set
        {
            theme = value;

            if (OnThemeChanged != null)
                OnThemeChanged();
        }
    }

    public static void SetThemeWithAlias(string theme)
    {
        var newTheme = GetAllThemes.FirstOrDefault(u => u.ThemeName == theme);

        if (newTheme != null)
            Theme = newTheme;
        else
            Theme = new DarkTheme();
    }

    private static void LoadAddtionalThemes()
    {
        if (ManagerOS.IsWindows10OrWindows11)
        {
            var accentDark = new CustomAccentDarkTheme() { Enabled = true };
            var accentLight = new CustomAccentLightTheme() { Enabled = true };

            accentDark.Colors.MainAccent = ManagerOS.GetAccentColor();
            accentDark.Colors.DockActive = System.Windows.Forms.ControlPaint.Dark(ManagerOS.GetAccentColor(), 0.1f);

            accentLight.Colors.MainAccent = ManagerOS.GetAccentColor();
            accentLight.Colors.DockActive = System.Windows.Forms.ControlPaint.Dark(ManagerOS.GetAccentColor(), 0.1f);

            _allThemes.Add(accentDark);
            _allThemes.Add(accentLight);
        }
    }

    /// <summary>
    /// Returns a list with all available themes
    /// <br><i> NOTE: Disabled themes won't appear in this list </i></br>
    /// </summary>
    public static List<ITheme> GetAllThemes => _allThemes;
}
