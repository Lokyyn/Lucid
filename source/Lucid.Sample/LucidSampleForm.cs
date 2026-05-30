using Lucid.Sample.Pages;
using Lucid.Sample.SideViews;
using Lucid.Win32;

namespace Lucid.Sample;

public partial class LucidSampleForm : Form
{
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

        AddToolWindows();
        AddPages();
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
