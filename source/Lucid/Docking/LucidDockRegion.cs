using Lucid.Theming;
using System.ComponentModel;

namespace Lucid.Docking;

[ToolboxItem(false)]
public class LucidDockRegion : Panel
{
    #region Field Region

    private List<LucidDockGroup> _groups;

    private Form _parentForm;
    private LucidDockSplitter _splitter;

    #endregion

    #region Property Region

    public LucidDockPanel DockPanel { get; private set; }

    public LucidDockArea DockArea { get; private set; }

    public LucidDockContent ActiveDocument
    {
        get
        {
            if (DockArea != LucidDockArea.Document || _groups.Count == 0)
                return null;

            return _groups[0].VisibleContent;
        }
    }

    public List<LucidDockGroup> Groups
    {
        get
        {
            return _groups.ToList();
        }
    }

    #endregion

    #region Constructor Region

    public LucidDockRegion(LucidDockPanel dockPanel, LucidDockArea dockArea)
    {
        _groups = new List<LucidDockGroup>();

        DockPanel = dockPanel;
        DockArea = dockArea;

        BuildProperties();
    }

    #endregion

    #region Method Region

    internal void AddContent(LucidDockContent dockContent)
    {
        AddContent(dockContent, null);
    }

    internal void AddContent(LucidDockContent dockContent, LucidDockGroup dockGroup)
    {
        // If no existing group is specified then create a new one
        if (dockGroup == null)
        {
            // If this is the document region, then default to first group if it exists
            if (DockArea == LucidDockArea.Document && _groups.Count > 0)
                dockGroup = _groups[0];
            else
                dockGroup = CreateGroup();
        }

        dockContent.DockRegion = this;
        dockGroup.AddContent(dockContent);

        if (!Visible)
        {
            Visible = true;
            CreateSplitter();
        }

        PositionGroups();
    }

    internal void InsertContent(LucidDockContent dockContent, LucidDockGroup dockGroup, DockInsertType insertType)
    {
        var order = dockGroup.Order;

        if (insertType == DockInsertType.After)
            order++;

        var newGroup = InsertGroup(order);

        dockContent.DockRegion = this;
        newGroup.AddContent(dockContent);

        if (!Visible)
        {
            Visible = true;
            CreateSplitter();
        }

        PositionGroups();
    }

    internal void RemoveContent(LucidDockContent dockContent)
    {
        dockContent.DockRegion = null;

        var group = dockContent.DockGroup;
        group.RemoveContent(dockContent);

        dockContent.DockArea = LucidDockArea.None;

        // If that was the final content in the group then remove the group
        if (group.ContentCount == 0)
            RemoveGroup(group);

        // If we just removed the final group, and this isn't the document region, then hide
        if (_groups.Count == 0 && DockArea != LucidDockArea.Document)
        {
            Visible = false;
            RemoveSplitter();
        }

        PositionGroups();
    }

    public List<LucidDockContent> GetContents()
    {
        var result = new List<LucidDockContent>();
        
        foreach (var group in _groups)
            result.AddRange(group.GetContents());

        return result;
    }

    private LucidDockGroup CreateGroup()
    {
        var order = 0;

        if (_groups.Count >= 1)
        {
            order = -1;
            foreach (var group in _groups)
            {
                if (group.Order >= order)
                    order = group.Order + 1;
            }
        }

        var newGroup = new LucidDockGroup(DockPanel, this, order);
        _groups.Add(newGroup);
        Controls.Add(newGroup);

        return newGroup;
    }

    private LucidDockGroup InsertGroup(int order)
    {
        foreach (var group in _groups)
        {
            if (group.Order >= order)
                group.Order++;
        }

        var newGroup = new LucidDockGroup(DockPanel, this, order);
        _groups.Add(newGroup);
        Controls.Add(newGroup);

        return newGroup;
    }

    private void RemoveGroup(LucidDockGroup group)
    {
        var lastOrder = group.Order;

        _groups.Remove(group);
        Controls.Remove(group);

        foreach (var otherGroup in _groups)
        {
            if (otherGroup.Order > lastOrder)
                otherGroup.Order--;
        }
    }

    private void PositionGroups()
    {
        DockStyle dockStyle;

        switch (DockArea)
        {
            default:
            case LucidDockArea.Document:
                dockStyle = DockStyle.Fill;
                break;
            case LucidDockArea.Left:
            case LucidDockArea.Right:
                dockStyle = DockStyle.Top;
                break;
            case LucidDockArea.Bottom:
                dockStyle = DockStyle.Left;
                break;
        }

        if (_groups.Count == 1)
        {
            _groups[0].Dock = DockStyle.Fill;
            return;
        }

        if (_groups.Count > 1)
        {
            var lastGroup = _groups.OrderByDescending(g => g.Order).First();

            foreach (var group in _groups.OrderByDescending(g => g.Order))
            {
                group.SendToBack();

                if (group.Order == lastGroup.Order)
                    group.Dock = DockStyle.Fill;
                else
                    group.Dock = dockStyle;
            }

            SizeGroups();
        }
    }

    private void SizeGroups()
    {
        if (_groups.Count <= 1)
            return;

        var size = new Size(0, 0);

        switch (DockArea)
        {
            default:
            case LucidDockArea.Document:
                return;
            case LucidDockArea.Left:
            case LucidDockArea.Right:
                size = new Size(ClientRectangle.Width, ClientRectangle.Height / _groups.Count);
                break;
            case LucidDockArea.Bottom:
                size = new Size(ClientRectangle.Width / _groups.Count, ClientRectangle.Height);
                break;
        }

        foreach (var group in _groups)
            group.Size = size;
    }

    private void BuildProperties()
    {
        MinimumSize = new Size(50, 50);

        switch (DockArea)
        {
            default:
            case LucidDockArea.Document:
                Dock = DockStyle.Fill;
                Padding = new Padding(0, 1, 0, 0);
                break;
            case LucidDockArea.Left:
                Dock = DockStyle.Left;
                Padding = new Padding(0, 0, 1, 0);
                Visible = false;
                break;
            case LucidDockArea.Right:
                Dock = DockStyle.Right;
                Padding = new Padding(1, 0, 0, 0);
                Visible = false;
                break;
            case LucidDockArea.Bottom:
                Dock = DockStyle.Bottom;
                Padding = new Padding(0, 0, 0, 0);
                Visible = false;
                break;
        }
    }

    private void CreateSplitter()
    {
        if (_splitter != null && DockPanel.Splitters.Contains(_splitter))
            DockPanel.Splitters.Remove(_splitter);

        switch (DockArea)
        {
            case LucidDockArea.Left:
                _splitter = new LucidDockSplitter(DockPanel, this, LucidSplitterType.Right);
                break;
            case LucidDockArea.Right:
                _splitter = new LucidDockSplitter(DockPanel, this, LucidSplitterType.Left);
                break;
            case LucidDockArea.Bottom:
                _splitter = new LucidDockSplitter(DockPanel, this, LucidSplitterType.Top);
                break;
            default:
                return;
        }

        DockPanel.Splitters.Add(_splitter);
    }

    private void RemoveSplitter()
    {
        if (DockPanel.Splitters.Contains(_splitter))
            DockPanel.Splitters.Remove(_splitter);
    }

    #endregion

    #region Event Handler Region

    protected override void OnCreateControl()
    {
        base.OnCreateControl();

        _parentForm = FindForm();
        _parentForm.ResizeEnd += ParentForm_ResizeEnd;
    }

    protected override void OnResize(EventArgs eventargs)
    {
        base.OnResize(eventargs);

        SizeGroups();
    }

    private void ParentForm_ResizeEnd(object sender, EventArgs e)
    {
        if (_splitter != null)
            _splitter.UpdateBounds();
    }

    protected override void OnLayout(LayoutEventArgs e)
    {
        base.OnLayout(e);

        if (_splitter != null)
            _splitter.UpdateBounds();
    }

    #endregion

    #region Paint Region

    public void Redraw()
    {
        Invalidate();

        foreach (var group in _groups)
            group.Redraw();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;

        if (!Visible)
            return;

        // Fill body
        using (var b = new SolidBrush(ThemeProvider.Theme.Colors.MainBackgroundColor))
        {
            g.FillRectangle(b, ClientRectangle);
        }

        // Draw border
        using (var p = new Pen(ThemeProvider.Theme.Colors.DarkBorder))
        {
            // Top border
            if (DockArea == LucidDockArea.Document)
                g.DrawLine(p, ClientRectangle.Left, 0, ClientRectangle.Right, 0);

            // Left border
            if (DockArea == LucidDockArea.Right)
                g.DrawLine(p, ClientRectangle.Left, 0, ClientRectangle.Left, ClientRectangle.Height);

            // Right border
            if (DockArea == LucidDockArea.Left)
                g.DrawLine(p, ClientRectangle.Right - 1, 0, ClientRectangle.Right - 1, ClientRectangle.Height);
        }
    }

    #endregion
}
