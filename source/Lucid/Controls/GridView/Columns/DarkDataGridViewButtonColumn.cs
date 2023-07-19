using System.ComponentModel;
using System.Reflection;
using Lucid.Theming;

namespace Lucid.Controls.GridView.Columns
{
    public class DarkDataGridViewButtonColumn : DataGridViewButtonColumn
    {
        private bool _enabled = true;

        public DarkDataGridViewButtonColumn()
        {
            CellTemplate = new DarkDataGridViewButtonCell();
            base.UseColumnTextForButtonValue = true;
            base.FlatStyle = FlatStyle.Flat;
        }

        public sealed override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set { base.CellTemplate = value; }
        }

        [DefaultValue(FlatStyle.Flat)]
        public new FlatStyle FlatStyle
        {
            get { return FlatStyle.Flat; }
        }

        [DefaultValue(true)]
        public new bool UseColumnTextForButtonValue
        {
            get { return base.UseColumnTextForButtonValue; }
            set { base.UseColumnTextForButtonValue = value; }
        }

        [DefaultValue(true)]
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (value == _enabled)
                    return;
                _enabled = value;
                DataGridView?.InvalidateColumn(Index);
            }
        }
    }

    public class DarkDataGridViewButtonCell : DataGridViewButtonCell
    {
        // Unfortunately we need access to a private data member
        private static readonly PropertyInfo _buttonState = typeof(DataGridViewButtonCell).GetProperty("ButtonState", BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly Padding _padding = new Padding(1, 1, 2, 2);
        private static readonly StringFormat _stringFormat = new StringFormat
        {
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Center,
            Trimming = StringTrimming.EllipsisCharacter
        };

        private int? _mouseCurserCell;
        private bool? _enabled;

        [Browsable(false)]
        public ButtonState ButtonState => (ButtonState)_buttonState.GetValue(this, null);

        [DefaultValue(false)]
        public bool Enabled
        {
            get
            {
                return _enabled ?? (OwningColumn as DarkDataGridViewButtonColumn)?.Enabled ?? true;
            }
            set
            {
                if (value == Enabled)
                    return;
                _enabled = value;
                DataGridView?.InvalidateCell(this);
            }
        }


        protected override void OnMouseEnter(int rowIndex)
        {
            base.OnMouseEnter(rowIndex);

            int? previousMousePositionCellCell = _mouseCurserCell;
            _mouseCurserCell = rowIndex;

            // Update
            if (previousMousePositionCellCell.HasValue)
            {
                DataGridView.InvalidateCell(ColumnIndex, previousMousePositionCellCell.Value);
                DataGridView.InvalidateCell(ColumnIndex, RowIndex);
            }
            DataGridView.InvalidateCell(ColumnIndex, RowIndex);
        }

        protected override void OnMouseLeave(int rowIndex)
        {
            base.OnMouseLeave(rowIndex);

            _mouseCurserCell = null;
            DataGridView.InvalidateCell(ColumnIndex, RowIndex);
        }

        protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
        {
            if (Enabled)
                base.OnKeyDown(e, rowIndex);
        }

        protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
        {
            if (Enabled)
                base.OnMouseDown(e);
        }


        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates elementState, object value, object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            if (FlatStyle != FlatStyle.Flat)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value,
                    formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
                return;
            }

            // Paint background
            if (paintParts.HasFlag(DataGridViewPaintParts.Background))
            {
                Color backColor = elementState.HasFlag(DataGridViewElementStates.Selected) ?
                cellStyle.SelectionBackColor : cellStyle.BackColor;
                using (var brush = new SolidBrush(backColor))
                    graphics.FillRectangle(brush, cellBounds);
            }

            if (paintParts.HasFlag(DataGridViewPaintParts.Border))
                PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);

            // Choose button colors
            Color textColor = cellStyle.ForeColor;
            Color borderColor = ThemeProvider.Theme.Colors.GreySelection;
            Color fillColor = ThemeProvider.Theme.Colors.MainBackgroundColor;

            if (DataGridView.Focused && DataGridView.CurrentCellAddress == new Point(ColumnIndex, rowIndex))
                borderColor = ThemeProvider.Theme.Colors.MainAccent; //Selection

            if (ButtonState.HasFlag(ButtonState.Inactive) || !Enabled)
            {
                fillColor = ThemeProvider.Theme.Colors.DarkGreySelection;
                textColor = ThemeProvider.Theme.Colors.DisabledText;
            }
            else if (ButtonState.HasFlag(ButtonState.Checked) || ButtonState.HasFlag(ButtonState.Pushed))
                fillColor = ThemeProvider.Theme.Colors.DarkBackground;
            else if (_mouseCurserCell == rowIndex) // Hover
                fillColor = ThemeProvider.Theme.Colors.LighterBackground;

            // Paint button
            Rectangle contentBounds = new Rectangle(cellBounds.X + _padding.Left, cellBounds.Y + _padding.Top,
                cellBounds.Width - _padding.Horizontal, cellBounds.Height - _padding.Vertical);

            if (paintParts.HasFlag(DataGridViewPaintParts.ContentBackground))
            {
                using (var brush = new SolidBrush(fillColor))
                    graphics.FillRectangle(brush, contentBounds);

                using (var pen = new Pen(borderColor, 1))
                    graphics.DrawRectangle(pen, contentBounds.Left, contentBounds.Top, contentBounds.Width - 1, contentBounds.Height - 1);
            }

            if (paintParts.HasFlag(DataGridViewPaintParts.ContentForeground))
                using (var brush = new SolidBrush(textColor))
                    graphics.DrawString(formattedValue?.ToString() ?? "", cellStyle.Font, brush, contentBounds, _stringFormat);

            // Paint error
            if (DataGridView.ShowCellErrors && paintParts.HasFlag(DataGridViewPaintParts.ErrorIcon))
                PaintErrorIcon(graphics, clipBounds, contentBounds, errorText);
        }
    }
}
