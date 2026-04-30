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
}
