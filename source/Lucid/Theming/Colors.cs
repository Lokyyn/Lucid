using System.Drawing;

namespace Lucid.Theming
{
    public class Colors
    {
        // New

        /// <summary>
        /// Defines the background color for docks
        /// </summary>
        public Color DockBackground { get; set; }

        /// <summary>
        /// Defines the color for odd rows (e.g. Listview)
        /// </summary>
        public Color RowOdd { get; set; }

        /// <summary>
        /// Defines the color for even rows (e.g. Listview)
        /// </summary>
        public Color RowEven { get; set; }

        /// <summary>
        /// Defines the color for the highlight area if a dock is moved
        /// </summary>
        public Color DockMovedHighlight { get; set; }

        /// <summary>
        /// Defines the header color when a dock is inactive
        /// </summary>
        public Color DockInactive { get; set; }

        /// <summary>
        /// Defines the header color when a dock is active
        /// </summary>
        public Color DockActive { get; set; }

        // New

        /// <summary>
        /// Defines the standard Control background color (Forms, Dockpanels, ...)
        /// </summary>
        /// <remarks>Old name: GreyBackground</remarks>
        public Color MainBackgroundColor { get; set; }

        public Color HeaderBackground { get; set; }

        public Color BlueBackground { get; set; }

        public Color DarkBlueBackground { get; set; }

        public Color DarkBackground { get; set; }

        public Color MediumBackground { get; set; }

        public Color LightBackground { get; set; }

        public Color LighterBackground { get; set; }

        public Color LightestBackground { get; set; }

        public Color LightBorder { get; set; }

        public Color DarkBorder { get; set; }

        public Color LightText { get; set; }

        public Color DisabledText { get; set; }

        /// <summary>
        /// Defines the highlight color for activ controls
        /// </summary>
        public Color ControlHighlight { get; set; }

        /// <summary>
        /// Defines the main accent color
        /// </summary>
        public Color MainAccent { get; set; }

        /// <summary>
        /// Defines the color that can be used for LinkLabels as an highlight color
        /// </summary>
        public Color LabelLinkAccent { get; set; }

        /// <summary>
        /// Defines the color that can be used for LinkLabels as an hovered highlight color
        /// </summary>
        public Color LabelLinkHoveredAccent { get; set; }

        public Color GreyHighlight { get; set; }

        public Color GreySelection { get; set; }

        public Color DarkGreySelection { get; set; }

        public Color DarkBlueBorder { get; set; }

        public Color LightBlueBorder { get; set; }

        /// <summary>
        /// The active color of an scrollbar
        /// </summary>
        public Color ActiveControl { get; set; }

        /// <summary>
        /// The inactiv color for an scroll bar
        /// </summary>
        public Color InactivScrollbar { get; set; }

        /// <summary>
        /// The hit color for an scroll bar
        /// </summary>
        public Color HotScrollbar { get; set; }
    }
}
