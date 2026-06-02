using Lucid.Controls.DataClasses.BadgeProgressbar;
using Lucid.Docking;
using Lucid.Theming;

namespace Lucid.Sample.Pages;

public partial class MainPage : LucidDocument
{
    Random r = new Random();

    private Controls.LucidTreeNode? _progressNode1;
    private Controls.LucidTreeNode? _progressNode2;
    private Controls.LucidTreeNode? _progressNode3;

    List<Color> _colors = new List<Color>()
    {
        Color.SkyBlue,
        Color.BlueViolet,
        Color.Orange,
        Color.ForestGreen,
        Color.Yellow,
        Color.IndianRed,
        Color.Teal
    };

    List<string> _names = new List<string>()
    {
        "New Arrival",
        "Limited Edition",
        "Top Rated",
        "Out of Stock",
        "Best Seller",
        "Free Shipping",
        "In Stock",
        "Refurbished",
        "On Sale",
        "International Shipping",
        "Same-Day Delivery"
    };

    private Controls.LucidPerformanceToolTip? _ttRevenue;
    private Controls.LucidPerformanceToolTip? _ttVisits;
    private Controls.LucidPerformanceToolTip? _ttChurn;

    public MainPage()
    {
        InitializeComponent();

        SetUpChipControl();
        SetUpComboBox();
        SetUpTreeView();
        SetUpTextBoxes();
        SetUpPerformanceToolTips();
    }

    private void SetUpPerformanceToolTips()
    {
        _ttRevenue = new Controls.LucidPerformanceToolTip { Difference = 15.3 };
        _ttRevenue.Set(lbTTDemo1, "Revenue");

        _ttVisits = new Controls.LucidPerformanceToolTip { Difference = 0 };
        _ttVisits.Set(lbTTDemo2, "Visits");

        _ttChurn = new Controls.LucidPerformanceToolTip { Difference = -8.7 };
        _ttChurn.Set(lbTTDemo3, "Churn");

        // plain themed tooltip — no Difference set
        var plainTooltip = new Controls.LucidPerformanceToolTip();
        plainTooltip.SetToolTip(lucidButtonNormal, "Themed tooltip without performance indicator");
    }

    private void SetUpTextBoxes()
    {
        lucidTextBox2.Text = "Clear me";
    }

    private void SetUpComboBox()
    {
        lucidComboBox1.Items.AddRange(new object[] { "Dark Theme", "Light Theme", "Custom Theme", "Some other", "item", "in this", "control.", "This", "list", "has", "many", "entries" });
        lucidComboBox1.SelectedIndex = 0;
    }

    private void SetUpTreeView()
    {
        var nodeAssets = new Controls.LucidTreeNode("Assets");
        nodeAssets.Nodes.Add(new Controls.LucidTreeNode("Icons"));
        nodeAssets.Nodes.Add(new Controls.LucidTreeNode("Fonts"));

        var nodeControls = new Controls.LucidTreeNode("Controls");
        nodeControls.Nodes.Add(new Controls.LucidTreeNode("LucidButton"));
        nodeControls.Nodes.Add(new Controls.LucidTreeNode("LucidCheckBox"));
        nodeControls.Nodes.Add(new Controls.LucidTreeNode("LucidTreeView"));

        var nodeTheming = new Controls.LucidTreeNode("Theming");
        nodeTheming.Nodes.Add(new Controls.LucidTreeNode("ThemeProvider"));
        nodeTheming.Nodes.Add(new Controls.LucidTreeNode("LucidTheme"));

        lucidTreeView1.Nodes.Add(nodeAssets);
        lucidTreeView1.Nodes.Add(nodeControls);
        lucidTreeView1.Nodes.Add(nodeTheming);

        _progressNode1 = new Controls.LucidTreeNode("Download");
        _progressNode1.ShowProgressBar = true;
        _progressNode1.ProgressbarSize = eProgressbarSize.Small;
        _progressNode1.ProgressBarPercentage = 30;

        _progressNode2 = new Controls.LucidTreeNode("Upload");
        _progressNode2.ShowProgressBar = true;
        _progressNode2.ProgressbarSize = eProgressbarSize.Medium;
        _progressNode2.ProgressBarPercentage = 30;

        _progressNode3 = new Controls.LucidTreeNode("Processing");
        _progressNode3.ShowProgressBar = true;
        _progressNode3.ProgressbarSize = eProgressbarSize.Large;
        _progressNode3.ProgressBarPercentage = 30;

        lucidTreeView1.Nodes.Add(_progressNode1);
        lucidTreeView1.Nodes.Add(_progressNode2);
        lucidTreeView1.Nodes.Add(_progressNode3);
    }

    private void SetUpChipControl()
    {
        lucidChipControl1.Chips.Add(new Controls.Chip() { Text = _names[0], BackColor = Color.SkyBlue });
        lucidChipControl1.Chips.Add(new Controls.Chip() { Text = _names[1], BackColor = Color.BlueViolet });
        lucidChipControl1.Chips.Add(new Controls.Chip() { Text = _names[2], BackColor = Color.Orange });
        lucidChipControl1.Chips.Add(new Controls.Chip() { Text = _names[3], BackColor = Color.ForestGreen });
        lucidChipControl1.Chips.Add(new Controls.Chip() { Text = _names[4], BackColor = Color.Yellow });
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        BackColor = ThemeProvider.Theme.Colors.BackgroundSecondary;
    }

    private void btAddChip_Click(object sender, EventArgs e)
    {
        var chipColor = _colors[r.Next(0, _colors.Count - 1)];
        var chipText = _names[r.Next(0, _names.Count - 1)];

        lucidChipControl1.Chips.Add(new Lucid.Controls.Chip() { Text = chipText, BackColor = chipColor });
        lucidChipControl1.Refresh();
    }

    private void btRemoveChip_Click(object sender, EventArgs e)
    {
        lucidChipControl1.Chips.RemoveAt(r.Next(0, lucidChipControl1.Chips.Count - 1));
        lucidChipControl1.Refresh();
    }

    private void lucidChipControl1_OnChipDeleted(Controls.Chip deletedChip)
    {

    }

    private void btnProgressBarAdd_Click(object sender, EventArgs e)
    {
        Random rnd = new Random();

        lucidProgressBar.Value += rnd.Next(6, 20);
    }

    private void btnProgressBarRemove_Click(object sender, EventArgs e)
    {
        Random rnd = new Random();

        lucidProgressBar.Value -= rnd.Next(6, 20);
    }

    private void btnTreeViewProgressAdd_Click(object sender, EventArgs e)
    {
        if (_progressNode1 is null || _progressNode2 is null || _progressNode3 is null) return;
        var delta = r.Next(6, 20);
        _progressNode1.ProgressBarPercentage = Math.Min(100, _progressNode1.ProgressBarPercentage + delta);
        _progressNode2.ProgressBarPercentage = Math.Min(100, _progressNode2.ProgressBarPercentage + delta);
        _progressNode3.ProgressBarPercentage = Math.Min(100, _progressNode3.ProgressBarPercentage + delta);
        lucidTreeView1.Refresh();
    }

    private void btnTreeViewProgressRemove_Click(object sender, EventArgs e)
    {
        if (_progressNode1 is null || _progressNode2 is null || _progressNode3 is null) return;
        var delta = r.Next(6, 20);
        _progressNode1.ProgressBarPercentage = Math.Max(0, _progressNode1.ProgressBarPercentage - delta);
        _progressNode2.ProgressBarPercentage = Math.Max(0, _progressNode2.ProgressBarPercentage - delta);
        _progressNode3.ProgressBarPercentage = Math.Max(0, _progressNode3.ProgressBarPercentage - delta);
        lucidTreeView1.Refresh();
    }

    private void btnToggleSliderMode_Click(object sender, EventArgs e)
    {
        if (lucidSlider1.Mode == Lucid.Controls.LucidSliderMode.SingleValue)
            lucidSlider1.Mode = Lucid.Controls.LucidSliderMode.Range;
        else
            lucidSlider1.Mode = Lucid.Controls.LucidSliderMode.SingleValue;
    }
}
