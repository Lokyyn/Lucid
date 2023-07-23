namespace Lucid.Theming;

public interface ITheme
{
    /// <summary>
    /// Access to all the theme sizes
    /// </summary>
    Sizes Sizes { get; }
    
    /// <summary>
    /// Access to all the theme colors
    /// </summary>
    Colors Colors { get; }

    /// <summary>
    /// Defines wether this theme is based on dark or light colors
    /// </summary>
    ThemeType Type { get; }

    /// <summary>
    /// Optional string for storing a translation key
    /// </summary>
    string MultilanguageKey { get; }

    /// <summary>
    /// Optional string for storing a image key
    /// </summary>
    string ImageKey { get; }

    /// <summary>
    /// Indicates if the theme can be displayed in the UI
    /// </summary>
    bool Enabled { get; }

    /// <summary>
    /// Gets the current theme name
    /// </summary>
    string ThemeName { get; }

    /// <summary>
    /// Gets the current order number for the this theme
    /// </summary>
    int OrderNo { get; }
}

/// <summary>
/// This enum indicates wether a theme has dark or light colors
/// </summary>
public enum ThemeType
{
    Dark,
    Light
}
