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
        "Chip with some longer text",
        "A",
        "Sample",
        "Something",
        "Small",
        "ABC",
        "Text",
        "Click me!"
    };

    public MainPage()
    {
        InitializeComponent();

        SetUpChipControl();

        lucidScrollableControl1.Refresh();
    }

    private void SetUpChipControl()
    {
        lucidChipControl1.Chips.Add(new Controls.Chip() { Text = "Chip 1", BackColor = Color.SkyBlue });
        lucidChipControl1.Chips.Add(new Controls.Chip() { Text = "Click me!", BackColor = Color.BlueViolet });
        lucidChipControl1.Chips.Add(new Controls.Chip() { Text = "Chip 3", BackColor = Color.Orange });
        lucidChipControl1.Chips.Add(new Controls.Chip() { Text = "Chip 4", BackColor = Color.ForestGreen });
        lucidChipControl1.Chips.Add(new Controls.Chip() { Text = "Chip 5", BackColor = Color.Yellow });
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

        lucidProgressBar.Value += rnd.Next(1, 15);
    }

    private void btnProgressBarRemove_Click(object sender, EventArgs e)
    {
        Random rnd = new Random();

        lucidProgressBar.Value -= rnd.Next(1, 15);
    }
    
    private void btnShowMessageBox_Click(object sender, EventArgs e)
    {
        Lucid.Forms.LucidMessageBox.ShowInformation("This is just an test message with an long text that has no meaning", "Information");
    }
}
