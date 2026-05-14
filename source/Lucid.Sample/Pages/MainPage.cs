using Lucid.Docking;
using Lucid.Theming;

namespace Lucid.Sample.Pages;

public partial class MainPage : LucidDocument
{
    Random r = new Random();

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

    public MainPage()
    {
        InitializeComponent();

        SetUpChipControl();
        SetUpComboBox();
        SetUpTreeView();
    }

    private void SetUpComboBox()
    {
        lucidComboBox1.Items.AddRange(new object[] { "Dark Theme", "Light Theme", "Custom Theme" });
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
        BackColor = ThemeProvider.Theme.Colors.MainBackgroundColor;
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

    private void btnToggleSliderMode_Click(object sender, EventArgs e)
    {
        if (lucidSlider1.Mode == Lucid.Controls.LucidSliderMode.SingleValue)
            lucidSlider1.Mode = Lucid.Controls.LucidSliderMode.Range;
        else
            lucidSlider1.Mode = Lucid.Controls.LucidSliderMode.SingleValue;
    }
}
