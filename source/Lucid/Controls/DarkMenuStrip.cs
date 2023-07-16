using Lucid.Renderers;
using System.Windows.Forms;

namespace Lucid.Controls
{
    public class DarkMenuStrip : MenuStrip
    {
        #region Constructor Region

        public DarkMenuStrip()
        {
            Renderer = new DarkMenuRenderer();
            Padding = new Padding(3, 2, 0, 2);
        }

        #endregion
    }
}
