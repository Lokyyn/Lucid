using System.Drawing;

namespace Lucid.Theming
{
    public interface ITheme
    {
        Sizes Sizes { get; }
        
        Colors Colors { get; }

        ThemeType Type { get; }

        string MultilanguageKey { get; }

        string ImageKey { get; }

        /// <summary>
        /// Indicates if the language can be displayed in the UI
        /// </summary>
        bool Enabled { get; }

        string ThemeName { get; }

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
}
