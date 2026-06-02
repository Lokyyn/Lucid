using Lucid.Sample.Pages;
using Lucid.Sample.SideViews;
using Lucid.Win32;

namespace Lucid.Sample;

public partial class LucidSampleForm : Form
{
    private readonly Lucid.Controls.LucidPerformanceToolTip _menuTooltip = new();

    public LucidSampleForm()
    {
        InitializeComponent();

        // Enable draging and resizing
        Application.AddMessageFilter(new ControlScrollFilter());
        Application.AddMessageFilter(lucidDockPanel.DockContentDragFilter);
        Application.AddMessageFilter(lucidDockPanel.DockResizeFilter);

        //Theming.ThemeProvider.Theme = new Theming.Themes.DarkGreenTheme();
        //Theming.ThemeProvider.Theme = new Theming.Themes.DarkPurpleTheme();
        //Theming.ThemeProvider.Theme = new Theming.Themes.LightTealTheme();

        SetUpMenuTooltips();
        AddToolWindows();
        AddPages();
    }

    private void SetUpMenuTooltips()
    {
        switchToDarkToolStripMenuItem.MouseEnter  += (s, _) => ShowMenuTooltip((ToolStripItem)s, "Switches the application to the built-in dark theme");
        switchToDarkToolStripMenuItem.MouseLeave  += (_, _) => _menuTooltip.Hide(this);
        switchToLightToolStripMenuItem.MouseEnter += (s, _) => ShowMenuTooltip((ToolStripItem)s, "Switches the application to the built-in light theme");
        switchToLightToolStripMenuItem.MouseLeave += (_, _) => _menuTooltip.Hide(this);
    }

    private void ShowMenuTooltip(ToolStripItem item, string text)
    {
        if (item.Owner == null) return;
        var screen = item.Owner.PointToScreen(new Point(item.Bounds.Right, item.Bounds.Top));
        var local  = PointToClient(screen);
        _menuTooltip.Show(text, this, local.X + 4, local.Y);
    }

    private void AddPages()
    {
        lucidDockPanel.AddContent(new MainPage());
    }

    private void AddToolWindows()
    {
        lucidDockPanel.AddContent(new ToolWindow());
    }

    private void switchToDarkToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Theming.ThemeProvider.Theme = new Theming.Themes.DarkTheme();
        Refresh();
    }

    private void switchToLightToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Theming.ThemeProvider.Theme = new Theming.Themes.LightTheme();
        Refresh();
    }
}
