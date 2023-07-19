using System.Drawing.Drawing2D;

namespace Lucid.Common;

public class SaveableGraphicsState : IDisposable
{
    private GraphicsState _State;
    private Graphics _Graphics;

    /// <summary>
    /// Saves all modifications to the <see cref="Graphics"/> so it can then later savely be restored
    /// </summary>
    /// <param name="g"></param>
    public SaveableGraphicsState(Graphics g)
    {
        _Graphics = g;
        _State = g.Save();
    }

    /// <summary>
    /// When called it resets the state of the current <see cref="Graphics"/> object
    /// </summary>
    public void Dispose()
    {
        _Graphics.Restore(_State);
    }
}
