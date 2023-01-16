using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Controls.DataClasses.Badge
{
    public class BadgeColorCollection
    {
        private List<BadgeColor> _badgeColorsInner;

        public BadgeColorCollection()
        {
            _badgeColorsInner = new List<BadgeColor>();
        }

        public BadgeColor this[string colorId] => _badgeColorsInner.FirstOrDefault(u => u.ColorId == colorId);
        public BadgeColor this[int index] => _badgeColorsInner[index];

        /// <summary>
        /// Gets the corresponding <see cref="BadgeColor"/> that has the given colorId
        /// </summary>
        /// <param name="colorId"></param>
        /// <returns></returns>
        public BadgeColor GetColor(string colorId)
        {
            return _badgeColorsInner.FirstOrDefault(u => u.ColorId == colorId);
        }

        internal List<BadgeColor> BadgeColors => _badgeColorsInner;

        /// <summary>
        /// Adds an BadgeColor to the collection
        /// </summary>
        /// <param name="badgeId">The unique identifier of the badge</param>
        /// <param name="backColor">Represents the back fill color of the badge</param>
        /// <param name="foreColor">Represents the text color of the badge</param>
        /// <returns></returns>
        public BadgeColor AddColor(string badgeColorId, Color backColor, Color foreColor)
        {
            var badgeColor = new BadgeColor(badgeColorId, backColor, foreColor);
            _badgeColorsInner.Add(badgeColor);

            return badgeColor;
        }

        /// <summary>
        /// Adds an BadgeColor to the collection
        /// </summary>
        /// <param name="badgeColorId">The unique identifier of the badge</param>
        /// <param name="backColor">Represents the back fill color of the badge</param>
        /// <param name="hexForeColorCode">Represents the text color of the badge</param>
        /// <returns></returns>
        public BadgeColor AddColor(string badgeColorId, Color backColor, string hexForeColorCode)
        {
            var badgeColor = new BadgeColor(badgeColorId, backColor, hexForeColorCode);
            _badgeColorsInner.Add(badgeColor);

            return badgeColor;
        }

        /// <summary>
        /// Adds an BadgeColor to the collection
        /// </summary>
        /// <param name="badgeId">The unique identifier of the badge</param>
        /// <param name="hexBackColorCode">Represents the back fill color of the badge given in an HTML/Hex format</param>
        /// <param name="hexForeColorCode">Represents the text color of the badge given in an HTML/Hex format</param>
        /// <returns></returns>
        public BadgeColor AddColor(string badgeColorId, string hexBackColorCode, string hexForeColorCode)
        {
            var badgeColor = new BadgeColor(badgeColorId, hexBackColorCode, hexForeColorCode);
            _badgeColorsInner.Add(badgeColor);

            return badgeColor;
        }

        /// <summary>
        /// Adds the given badge color to the collection
        /// </summary>
        /// <param name="badge"></param>
        public void AddColor(BadgeColor badgeColor)
        {
            _badgeColorsInner.Add(badgeColor);
        }

        /// <summary>
        /// Removes the color with the given colorId
        /// </summary>
        /// <param name="colorId"></param>
        public void RemoveColor(string colorId)
        {
            var color = _badgeColorsInner.FirstOrDefault(u => u.ColorId == colorId);

            if (color != null)
                _badgeColorsInner.Remove(color);
        }
    }
}
