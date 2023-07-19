using Lucid.Renderers;

namespace Lucid.Controls;

public class LucidMenuStrip : MenuStrip
{
    #region Constructor Region

    public LucidMenuStrip()
    {
        Renderer = new LucidMenuRenderer();
        Padding = new Padding(3, 2, 0, 2);
    }

    #endregion
}
