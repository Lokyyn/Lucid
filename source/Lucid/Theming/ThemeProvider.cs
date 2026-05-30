using Lucid.Theming.Themes;
using Lucid.Theming.Themes.BaseThemes;

namespace Lucid.Theming;

public class ThemeProvider
{
    private static List<ITheme> _allThemes;
    private static List<ITheme> _userRegisteredThemes;
    private static ITheme theme;

    static ThemeProvider()
    {
        _allThemes = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(t => !t.IsInterface && (t.BaseType == typeof(BaseDarkTheme) || t.BaseType == typeof(BaseLightTheme))).Select(c => (ITheme)Activator.CreateInstance(c)).Where(u => u.Enabled).ToList();
        _userRegisteredThemes = new List<ITheme>();

        LoadAddtionalThemes();
    }

    /// <summary>
    /// Raised on the UI thread whenever <see cref="Theme"/> is replaced with a new value.
    /// Controls subscribe to this event to repaint themselves with the new color tokens.
    /// </summary>
    public static event ThemeChangedHandler OnThemeChanged;

    /// <summary>Callback signature for <see cref="OnThemeChanged"/>.</summary>
    public delegate void ThemeChangedHandler();

    /// <summary>
    /// Gets or sets the globally active theme.
    /// Setting this property raises <see cref="OnThemeChanged"/> so all subscribed controls repaint.
    /// Defaults to <see cref="DarkTheme"/> when first accessed.
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

    /// <summary>
    /// Activates the theme whose <see cref="ITheme.ThemeName"/> matches <paramref name="themeName"/>.
    /// Falls back to <see cref="DarkTheme"/> when no match is found.
    /// </summary>
    /// <param name="themeName">The exact theme name to look up (case-sensitive).</param>
    public static void SetThemeWithAlias(string themeName)
    {
        var newTheme = GetAllThemes.FirstOrDefault(u => u.ThemeName == themeName);

        if (newTheme != null)
            Theme = newTheme;
        else
            Theme = new DarkTheme();
    }

    /// <summary>
    /// Registers a custom theme so it appears in <see cref="GetAllThemes"/> and can be activated via <see cref="SetThemeWithAlias"/>.
    /// The theme must inherit from <c>BaseDarkTheme</c> or <c>BaseLightTheme</c>.
    /// </summary>
    /// <param name="theme">The theme instance to register.</param>
    /// <exception cref="NotSupportedException">Thrown when <paramref name="theme"/> does not inherit from a supported base theme.</exception>
    public static void RegisterTheme(ITheme theme)
    {
        if (theme.GetType().BaseType != typeof(BaseDarkTheme) && theme.GetType().BaseType != typeof(BaseLightTheme))
            throw new NotSupportedException($"This method only allows themes with inheritence of the base dark or light theme. For more information see {typeof(BaseLightTheme).Namespace}.");
        
        _userRegisteredThemes.Add(theme);
    }

    private static void LoadAddtionalThemes()
    {
        if (ManagerOS.IsWindows10OrWindows11)
        {
            var accentDark = new CustomAccentDarkTheme() { Enabled = true };
            var accentLight = new CustomAccentLightTheme() { Enabled = true };

            accentDark.Colors.Accent = ManagerOS.GetAccentColor();
            accentDark.Colors.Accent = System.Windows.Forms.ControlPaint.Dark(ManagerOS.GetAccentColor(), 0.1f);

            accentLight.Colors.Accent = ManagerOS.GetAccentColor();
            accentLight.Colors.Accent = System.Windows.Forms.ControlPaint.Dark(ManagerOS.GetAccentColor(), 0.1f);

            _allThemes.Add(accentDark);
            _allThemes.Add(accentLight);
        }
    }

    /// <summary>
    /// Returns all built-in themes plus any themes registered via <see cref="RegisterTheme"/>.
    /// Themes with <c>Enabled = false</c> are excluded.
    /// </summary>
    public static List<ITheme> GetAllThemes => new List<ITheme>(_allThemes).Concat(_userRegisteredThemes).ToList();
}
