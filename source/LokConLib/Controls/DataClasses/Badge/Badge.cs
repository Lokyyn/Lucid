using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Controls.DataClasses.Badge
{
    public class Badge
    {
        private string _badgeId;
        private string _value;
        private string _badgeColorId;
        private bool _visible;

        /// <summary>
        /// Creates an Badge-Object that contains all informations to display an badge in an TreeNode
        /// </summary>
        /// <param name="badgeId">The unique identifier of the badge</param>
        /// <param name="value">The text value that shall be displayed within the badge</param>
        /// <param name="badgeColor">The color object that is used for the visuals</param>
        public Badge(string badgeId, string value, BadgeColor badgeColor)
        {
            _badgeId = badgeId;
            _value = value;
            _badgeColorId = badgeColor.ColorId;
            _visible = true;
        }

        /// <summary>
        /// Creates an Badge-Object that contains all informations to display an badge in an TreeNode
        /// </summary>
        /// <param name="badgeId">The unique identifier of the badge</param>
        /// <param name="value">The text value that shall be displayed within the badge</param>
        /// <param name="badgeColor">The color object id that is used for the visuals</param>
        public Badge(string badgeId, string value, string badgeColorId)
        {
            _badgeId = badgeId;
            _value = value;
            _badgeColorId = badgeColorId;
            _visible = true;
        }

        public string BadgeId => _badgeId;
        public string Value
        {
            get => _value;
            set => _value = value;
        }
        public string BadgeColorId => _badgeColorId;
        public bool Visible => _visible;

        public bool ShowGradientIfAvailable { get; set; } = false;
    }
}
