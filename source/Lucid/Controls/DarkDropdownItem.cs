using System.Drawing;
using System.Windows.Forms;

namespace Lucid.Controls
{
    public class DarkDropdownItem
    {
        #region Property Region

        public string Text { get; set; }

        public Bitmap Icon { get; set; }

        public Color IconColor { get; set; }

        public object Tag { get; set; }

        #endregion

        #region Constructor Region

        public DarkDropdownItem()
        { }

        public DarkDropdownItem(string text)
        {
            Text = text;
        }

        public DarkDropdownItem(string text, Bitmap icon)
            : this(text)
        {
            Icon = icon;
        }

        public DarkDropdownItem(string text, Color iconColor)
            : this(text)
        {
            IconColor = iconColor;
        }

        #endregion
    }
}
