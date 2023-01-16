using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Controls.DataClasses.Badge
{
    public class BadgeColor
    {
        private string _colorId;
        private Color _backColor;
        private Color _backColor2;
        private Color _foreColor;

        /// <summary>
        /// Creates an BadgeColor object that is used for the visual aspect of an badge
        /// </summary>
        /// <param name="colorId">This Id is used to identify the ColorBadge-Object</param>
        /// <param name="backColor">Represents the back fill color of the badge</param>
        /// <param name="foreColor">Represents the text color of the badge</param>
        public BadgeColor(string colorId, Color backColor, Color foreColor)
        {
            _colorId = colorId;
            _backColor = backColor;
            _foreColor = foreColor;
        }

        /// <summary>
        /// Creates an BadgeColor object that is used for the visual aspect of an badge
        /// </summary>
        /// <param name="colorId">This Id is used to identify the ColorBadge-Object</param>
        /// <param name="backColor">Represents the back fill color of the badge</param>
        /// <param name="hexForeColor">Represents the text color of the badge</param>
        public BadgeColor(string colorId, Color backColor, string hexForeColorCode)
        {
            _colorId = colorId;
            _backColor = backColor;

            if (!hexForeColorCode.StartsWith('#'))
                hexForeColorCode = $"#{hexForeColorCode}";

            _foreColor = ColorTranslator.FromHtml(hexForeColorCode);
        }

        /// <summary>
        /// Creates an BadgeColor object that is used for the visual aspect of an badge
        /// </summary>
        /// <param name="colorId">This Id is used to identify the ColorBadge-Object</param>
        /// <param name="hexBackColorCode">Represents the back fill color of the badge given in an HTML/Hex format</param>
        /// <param name="hexForeColorCode">Represents the text color of the badge given in an HTML/Hex format</param>
        public BadgeColor(string colorId, string hexBackColorCode, string hexForeColorCode)
        {
            _colorId = colorId;

            if (!hexBackColorCode.StartsWith('#'))
                hexBackColorCode = $"#{hexBackColorCode}";

            if (!hexForeColorCode.StartsWith('#'))
                hexForeColorCode = $"#{hexForeColorCode}";

            _backColor = ColorTranslator.FromHtml(hexBackColorCode);
            _foreColor = ColorTranslator.FromHtml(hexForeColorCode);
        }

        public string ColorId => _colorId;
        public Color BackColor 
        {
            get => _backColor;
            set => _backColor = value; 
        }

        public Color BackColor2
        {
            get => _backColor2;
            set => _backColor2 = value;
        }

        public Color ForeColor
        {
            get => _foreColor;
            set => _foreColor = value;
        }
    }
}
