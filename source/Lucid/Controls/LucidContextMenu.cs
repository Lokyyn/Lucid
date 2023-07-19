using Lucid.Renderers;

namespace Lucid.Controls;

public class LucidContextMenu : ContextMenuStrip
{
    #region Constructor Region

    public LucidContextMenu()
    {
        Renderer = new LucidMenuRenderer();
    }

    #endregion
}
