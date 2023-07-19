namespace Lucid.Docking;

public class LucidControllableDockPanel : LucidDockPanel
{
    private static LucidControllableDockPanel _instance;

    private LucidControllableDockPanel()
    {

    }

    public static LucidControllableDockPanel GetInstance()
    {
        if (_instance == null)
            _instance = new LucidControllableDockPanel();

        return _instance;
    }
}
