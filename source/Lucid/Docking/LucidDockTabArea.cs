using Lucid.Controls;

namespace Lucid.Docking;

internal class LucidDockTabArea
{
    #region Field Region

    private Dictionary<LucidDockContent, LucidDockTab> _tabs = new Dictionary<LucidDockContent, LucidDockTab>();

    private List<ToolStripMenuItem> _menuItems = new List<ToolStripMenuItem>();
    private LucidContextMenu _tabMenu = new LucidContextMenu();

    private List<ToolStripMenuItem> _menuItemsDockSettings = new List<ToolStripMenuItem>();
    private LucidContextMenu _dockSettingsMenu = new LucidContextMenu();

    #endregion

    #region Property Region

    public LucidDockArea DockArea { get; private set; }

    public Rectangle ClientRectangle { get; set; }

    public Rectangle DropdownRectangle { get; set; }

    public Rectangle DockSettingsRectangle { get; set; }

    public bool DockSettingsVisible { get; set; } = false;

    public bool DropdownHot { get; set; }

    public bool DockSettingsHot { get; set; }

    public int Offset { get; set; }

    public int TotalTabSize { get; set; }

    public bool Visible { get; set; }

    public LucidDockTab ClickedCloseButton { get; set; }

    #endregion
    
    #region Constructor Region

    public LucidDockTabArea(LucidDockArea dockArea)
    {
        DockArea = dockArea;
    }

    #endregion

    #region Method Region

    public void ShowDockSettingsMenu(Control control, Point location)
    {
        InitDockSettingsMenu();

        _dockSettingsMenu.Show(control, location);
    }

    public void ShowMenu(Control control, Point location)
    {
        location.X = location.X - _tabMenu.Bounds.Width + 25; // Ensures that the menu is shown only in bounds of the form and on two monitors is not shown on an different monitor

        _tabMenu.Show(control, location);
    }

    public void AddMenuItem(ToolStripMenuItem menuItem)
    {
        _menuItems.Add(menuItem);
        RebuildMenu();
    }

    public void RemoveMenuItem(ToolStripMenuItem menuItem)
    {
        _menuItems.Remove(menuItem);
        RebuildMenu();
    }

    public ToolStripMenuItem GetMenuItem(LucidDockContent content)
    {
        ToolStripMenuItem menuItem = null;
        foreach (ToolStripMenuItem item in _menuItems)
        {
            var menuContent = item.Tag as LucidDockContent;
            if (menuContent == null)
                continue;

            if (menuContent == content)
                menuItem = item;
        }

        return menuItem;
    }

    public void RebuildMenu()
    {
        _tabMenu.Items.Clear();

        var orderedItems = new List<ToolStripMenuItem>();

        var index = 0;
        for (var i = 0; i < _menuItems.Count; i++)
        {
            foreach (var item in _menuItems)
            {
                var content = (LucidDockContent)item.Tag;
                if (content.Order == index)
                    orderedItems.Add(item);
            }
            index++;
        }

        foreach (var item in orderedItems)
            _tabMenu.Items.Add(item);
    }

    #endregion

    #region Private Methods

    private void InitDockSettingsMenu()
    {
        if (_menuItemsDockSettings.Count == 0)
        {
            var itemOpenTabInNewWindow = new ToolStripMenuItem("Show active tab in new window");
            itemOpenTabInNewWindow.Tag = "OpenTabInNewWindow";
            itemOpenTabInNewWindow.Image = DockIcons.BringToNewWindow;
            itemOpenTabInNewWindow.Click += DockSettingsItem_Click;

            var itemOpenTabInMainWindow = new ToolStripMenuItem("Open active tab in main window");
            itemOpenTabInMainWindow.Tag = "OpenTabInMainWindow";
            itemOpenTabInMainWindow.Image = DockIcons.BringToMainWindow;
            itemOpenTabInMainWindow.Click += DockSettingsItem_Click;

            var itemCloseCurrentTab = new ToolStripMenuItem("Close current active tab");
            itemCloseCurrentTab.Tag = "CloseCurrentTab";
            itemCloseCurrentTab.Click += DockSettingsItem_Click;

            var itemCloseAllWindows = new ToolStripMenuItem("Close all other windows");
            itemCloseAllWindows.Tag = "CloseAllWindows";
            itemCloseAllWindows.Click += DockSettingsItem_Click;

            _menuItemsDockSettings.Add(itemOpenTabInNewWindow);
            _menuItemsDockSettings.Add(itemOpenTabInNewWindow);
            _menuItemsDockSettings.Add(itemCloseCurrentTab);
            _menuItemsDockSettings.Add(itemCloseAllWindows);

            _dockSettingsMenu.Items.Add(itemOpenTabInNewWindow);
            _dockSettingsMenu.Items.Add(itemOpenTabInMainWindow);
            _dockSettingsMenu.Items.Add(itemCloseCurrentTab);
            _dockSettingsMenu.Items.Add(itemCloseAllWindows);
        }
    }

    #endregion

    #region DockSettings - Region

    private void DockSettingsItem_Click(object sender, System.EventArgs e)
    {
        var tag = (sender as ToolStripMenuItem).Tag as string;

        if (tag == "OpenTabInNewWindow")
        {

        }
        else if (tag == "OpenTabInMainWindow")
        {

        }
        else if (tag == "CloseCurrentTab")
        {

        }
        else if (tag == "CloseAllWinodws")
        {

        }
    }

    #endregion
}
