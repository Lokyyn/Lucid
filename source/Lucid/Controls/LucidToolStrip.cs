using Lucid.Renderers;

namespace Lucid.Controls;

public class LucidToolStrip : ToolStrip
{
    #region Constructor Region

    public LucidToolStrip()
    {
        Renderer = new LucidToolStripRenderer();
        Padding = new Padding(5, 0, 1, 0);
        AutoSize = false;
        Size = new Size(1, 28);
    }

    #endregion
}
