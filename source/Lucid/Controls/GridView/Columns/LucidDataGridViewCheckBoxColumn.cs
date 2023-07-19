using System.ComponentModel;

namespace Lucid.Controls.GridView.Columns;

public class LucidDataGridViewCheckBoxColumn : DataGridViewCheckBoxColumn
{
    public LucidDataGridViewCheckBoxColumn()
    {
        base.FlatStyle = FlatStyle.Flat;
    }

    [DefaultValue(FlatStyle.Flat)]
    public new FlatStyle FlatStyle
    {
        get { return FlatStyle.Flat; }
    }

    public class LucidDataGridViewCheckBoxCell : DataGridViewCheckBoxCell
    { }
}
