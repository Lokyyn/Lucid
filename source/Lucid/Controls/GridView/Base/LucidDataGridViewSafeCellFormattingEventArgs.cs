namespace Lucid.Controls.GridView.Base;

public class LucidDataGridViewSafeCellFormattingEventArgs : DataGridViewCellFormattingEventArgs
{
    public DataGridViewColumn Column { get; }
    public DataGridViewRow Row { get; }

    public LucidDataGridViewSafeCellFormattingEventArgs(int columnIndex, int rowIndex, object value,
        Type desiredType, DataGridViewCellStyle style, DataGridViewColumn column, DataGridViewRow row)
        : base(columnIndex, rowIndex, value, desiredType, style)
    {
        Column = column;
        Row = row;
    }
}


public delegate void LucidDataGridViewSafeCellFormattingEventHandler(object sender, LucidDataGridViewSafeCellFormattingEventArgs e);
