using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LCL.Controls.DataClasses.Badge;

namespace LCL.Controls.GridView.Columns
{
    public class DarkDataGridViewBadgeColumn : DataGridViewColumn
    {
        public BadgeCollection Badges { get; set; }

        public DarkDataGridViewBadgeColumn()
        {
            this.CellTemplate = new DarkDataGridViewBadgeCell();
        }
    }

    public class DarkDataGridViewBadgeCell : DataGridViewTextBoxCell
    {
        public DarkDataGridViewBadgeCell()
        {
            this.ToolTipText = "";
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, "", errorText, cellStyle, advancedBorderStyle, paintParts);

            var badgeCollectionValue = Value as BadgeCollection;

            if (Value != null && badgeCollectionValue.Badges.Count > 0)
                Renderers.BadgeRenderer.RenderForGridColumn(graphics, new Rectangle(new Point(cellBounds.X + 2, cellBounds.Y + 2), new Size(cellBounds.Width, cellBounds.Height)), badgeCollectionValue);
        }
    }
}
