namespace Lucid.Helper;

public static class ColorExtender
{
    /// <summary>
    /// Return either grey or black as contrast color
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static Color GetContrastColor(Color color)
    {
        return (color.R * 0.299M) + (color.G * 0.587M) + (color.B * 0.114M) > 130 ? ColorTranslator.FromHtml("#080808") : ColorTranslator.FromHtml("#dcdcdc");
    }

    /// <summary>
    /// Return either black or white as contrast color
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static Color GetContrastColorBW(Color color)
    {
        return (color.R * 0.299M) + (color.G * 0.587M) + (color.B * 0.114M) > 130 ? Color.Black : Color.White;
    }
}
