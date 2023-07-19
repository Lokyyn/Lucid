namespace Lucid.Docking;

internal class DockDropArea
{
    #region Property Region

    internal LucidDockPanel DockPanel { get; private set; }

    internal Rectangle DropArea { get; private set; }

    internal Rectangle HighlightArea { get; private set; }

    internal LucidDockRegion DockRegion { get; private set; }

    internal LucidDockGroup DockGroup { get; private set; }

    internal DockInsertType InsertType { get; private set; }

    #endregion

    #region Constructor Region

    internal DockDropArea(LucidDockPanel dockPanel, LucidDockRegion region)
    {
        DockPanel = dockPanel;
        DockRegion = region;
        InsertType = DockInsertType.None;

        BuildAreas();
    }

    internal DockDropArea(LucidDockPanel dockPanel, LucidDockGroup group, DockInsertType insertType)
    {
        DockPanel = dockPanel;
        DockGroup = group;
        InsertType = insertType;

        BuildAreas();
    }

    #endregion

    #region Method Region

    internal void BuildAreas()
    {
        if (DockRegion != null)
            BuildRegionAreas();
        else if (DockGroup != null)
            BuildGroupAreas();
    }

    private void BuildRegionAreas()
    {
        switch (DockRegion.DockArea)
        {
            case LucidDockArea.Left:

                var leftRect = new Rectangle
                {
                    X = DockPanel.PointToScreen(Point.Empty).X,
                    Y = DockPanel.PointToScreen(Point.Empty).Y,
                    Width = 250,
                    Height = DockPanel.Height
                };

                DropArea = leftRect;
                HighlightArea = leftRect;

                break;

            case LucidDockArea.Right:

                var rightRect = new Rectangle
                {
                    X = DockPanel.PointToScreen(Point.Empty).X + DockPanel.Width - 250,
                    Y = DockPanel.PointToScreen(Point.Empty).Y,
                    Width = 250,
                    Height = DockPanel.Height
                };

                DropArea = rightRect;
                HighlightArea = rightRect;

                break;

            case LucidDockArea.Bottom:

                var x = DockPanel.PointToScreen(Point.Empty).X;
                var width = DockPanel.Width;

                if (DockPanel.Regions[LucidDockArea.Left].Visible)
                {
                    x += DockPanel.Regions[LucidDockArea.Left].Width;
                    width -= DockPanel.Regions[LucidDockArea.Left].Width;
                }

                if (DockPanel.Regions[LucidDockArea.Right].Visible)
                {
                    width -= DockPanel.Regions[LucidDockArea.Right].Width;
                }

                var bottomRect = new Rectangle
                {
                    X = x,
                    Y = DockPanel.PointToScreen(Point.Empty).Y + DockPanel.Height - 150,
                    Width = width,
                    Height = 150
                };

                DropArea = bottomRect;
                HighlightArea = bottomRect;

                break;
        }
    }

    private void BuildGroupAreas()
    {
        switch (InsertType)
        {
            case DockInsertType.None:
                var dropRect = new Rectangle
                {
                    X = DockGroup.PointToScreen(Point.Empty).X,
                    Y = DockGroup.PointToScreen(Point.Empty).Y,
                    Width = DockGroup.Width,
                    Height = DockGroup.Height
                };

                DropArea = dropRect;
                HighlightArea = dropRect;

                break;

            case DockInsertType.Before:
                var beforeDropWidth = DockGroup.Width;
                var beforeDropHeight = DockGroup.Height;

                switch (DockGroup.DockArea)
                {
                    case LucidDockArea.Left:
                    case LucidDockArea.Right:
                        beforeDropHeight = DockGroup.Height / 4;
                        break;

                    case LucidDockArea.Bottom:
                        beforeDropWidth = DockGroup.Width / 4;
                        break;
                }

                var beforeDropRect = new Rectangle
                {
                    X = DockGroup.PointToScreen(Point.Empty).X,
                    Y = DockGroup.PointToScreen(Point.Empty).Y,
                    Width = beforeDropWidth,
                    Height = beforeDropHeight
                };

                DropArea = beforeDropRect;
                HighlightArea = beforeDropRect;

                break;

            case DockInsertType.After:
                var afterDropX = DockGroup.PointToScreen(Point.Empty).X;
                var afterDropY = DockGroup.PointToScreen(Point.Empty).Y;
                var afterDropWidth = DockGroup.Width;
                var afterDropHeight = DockGroup.Height;

                switch (DockGroup.DockArea)
                {
                    case LucidDockArea.Left:
                    case LucidDockArea.Right:
                        afterDropHeight = DockGroup.Height / 4;
                        afterDropY = DockGroup.PointToScreen(Point.Empty).Y + DockGroup.Height - afterDropHeight;
                        break;

                    case LucidDockArea.Bottom:
                        afterDropWidth = DockGroup.Width / 4;
                        afterDropX = DockGroup.PointToScreen(Point.Empty).X + DockGroup.Width - afterDropWidth;
                        break;
                }

                var afterDropRect = new Rectangle
                {
                    X = afterDropX,
                    Y = afterDropY,
                    Width = afterDropWidth,
                    Height = afterDropHeight
                };

                DropArea = afterDropRect;
                HighlightArea = afterDropRect;

                break;
        }
    }

    #endregion
}
