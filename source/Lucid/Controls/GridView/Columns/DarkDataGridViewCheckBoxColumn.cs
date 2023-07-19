using System.ComponentModel;

namespace Lucid.Controls.GridView.Columns
{
    public class DarkDataGridViewCheckBoxColumn : DataGridViewCheckBoxColumn
    {
        public DarkDataGridViewCheckBoxColumn()
        {
            base.FlatStyle = FlatStyle.Flat;
        }

        [DefaultValue(FlatStyle.Flat)]
        public new FlatStyle FlatStyle
        {
            get { return FlatStyle.Flat; }
        }

        public class DarkDataGridViewCheckBoxCell : DataGridViewCheckBoxCell
        { }
    }
}
