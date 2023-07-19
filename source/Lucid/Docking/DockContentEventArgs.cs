namespace Lucid.Docking;

public class DockContentEventArgs : EventArgs
{
    public LucidDockContent Content { get; private set; }

    public DockContentEventArgs(LucidDockContent content)
    {
        Content = content;
    }
}
