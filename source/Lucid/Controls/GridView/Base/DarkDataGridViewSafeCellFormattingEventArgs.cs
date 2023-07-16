using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lucid.Controls.GridView.Base
{
    public class DarkDataGridViewSafeCellFormattingEventArgs : DataGridViewCellFormattingEventArgs
    {
        public DataGridViewColumn Column { get; }
        public DataGridViewRow Row { get; }

        public DarkDataGridViewSafeCellFormattingEventArgs(int columnIndex, int rowIndex, object value,
            Type desiredType, DataGridViewCellStyle style, DataGridViewColumn column, DataGridViewRow row)
            : base(columnIndex, rowIndex, value, desiredType, style)
        {
            Column = column;
            Row = row;
        }
    }


    public delegate void DarkDataGridViewSafeCellFormattingEventHandler(object sender, DarkDataGridViewSafeCellFormattingEventArgs e);
}
