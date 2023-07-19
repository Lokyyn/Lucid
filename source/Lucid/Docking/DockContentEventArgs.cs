namespace Lucid.Docking
{
    public class DockContentEventArgs : EventArgs
    {
        public DarkDockContent Content { get; private set; }

        public DockContentEventArgs(DarkDockContent content)
        {
            Content = content;
        }
    }
}
