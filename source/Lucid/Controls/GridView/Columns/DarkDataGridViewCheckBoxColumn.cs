using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
