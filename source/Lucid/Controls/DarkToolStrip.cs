using Lucid.Renderers;

namespace Lucid.Controls
{
    public class DarkToolStrip : ToolStrip
    {
        #region Constructor Region

        public DarkToolStrip()
        {
            Renderer = new DarkToolStripRenderer();
            Padding = new Padding(5, 0, 1, 0);
            AutoSize = false;
            Size = new Size(1, 28);
        }

        #endregion
    }
}
