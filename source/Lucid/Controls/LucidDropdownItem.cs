namespace Lucid.Controls;

public class LucidDropdownItem
{
    #region Property Region

    public string Text { get; set; }

    public Bitmap Icon { get; set; }

    public Color IconColor { get; set; }

    public object Tag { get; set; }

    #endregion

    #region Constructor Region

    public LucidDropdownItem()
    { }

    public LucidDropdownItem(string text)
    {
        Text = text;
    }

    public LucidDropdownItem(string text, Bitmap icon)
        : this(text)
    {
        Icon = icon;
    }

    public LucidDropdownItem(string text, Color iconColor)
        : this(text)
    {
        IconColor = iconColor;
    }

    #endregion
}
