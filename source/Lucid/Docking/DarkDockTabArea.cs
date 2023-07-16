﻿using Lucid.Controls;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lucid.Docking
{
    internal class DarkDockTabArea
    {
        #region Field Region

        private Dictionary<DarkDockContent, DarkDockTab> _tabs = new Dictionary<DarkDockContent, DarkDockTab>();

        private List<ToolStripMenuItem> _menuItems = new List<ToolStripMenuItem>();
        private DarkContextMenu _tabMenu = new DarkContextMenu();

        private List<ToolStripMenuItem> _menuItemsDockSettings = new List<ToolStripMenuItem>();
        private DarkContextMenu _dockSettingsMenu = new DarkContextMenu();

        #endregion

        #region Property Region

        public DarkDockArea DockArea { get; private set; }

        public Rectangle ClientRectangle { get; set; }

        public Rectangle DropdownRectangle { get; set; }

        public Rectangle DockSettingsRectangle { get; set; }

        public bool DockSettingsVisible { get; set; } = false;

        public bool DropdownHot { get; set; }

        public bool DockSettingsHot { get; set; }

        public int Offset { get; set; }

        public int TotalTabSize { get; set; }

        public bool Visible { get; set; }

        public DarkDockTab ClickedCloseButton { get; set; }

        #endregion
        
        #region Constructor Region

        public DarkDockTabArea(DarkDockArea dockArea)
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

        public ToolStripMenuItem GetMenuItem(DarkDockContent content)
        {
            ToolStripMenuItem menuItem = null;
            foreach (ToolStripMenuItem item in _menuItems)
            {
                var menuContent = item.Tag as DarkDockContent;
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
                    var content = (DarkDockContent)item.Tag;
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
}