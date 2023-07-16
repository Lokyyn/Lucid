using Lucid.Renderers;
using System.Windows.Forms;

namespace Lucid.Controls
{
    public class DarkContextMenu : ContextMenuStrip
    {
        #region Constructor Region

        public DarkContextMenu()
        {
            Renderer = new DarkMenuRenderer();
        }

        #endregion
    }
}
