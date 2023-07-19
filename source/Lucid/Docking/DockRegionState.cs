namespace Lucid.Docking;

public class DockRegionState
{
    #region Property Region

    public LucidDockArea Area { get; set; }

    public Size Size { get; set; }

    public List<DockGroupState> Groups { get; set; }

    #endregion

    #region Constructor Region

    public DockRegionState()
    {
        Groups = new List<DockGroupState>();
    }

    public DockRegionState(LucidDockArea area)
        : this()
    {
        Area = area;
    }

    public DockRegionState(LucidDockArea area, Size size)
        : this(area)
    {
        Size = size;
    }

    #endregion
}
