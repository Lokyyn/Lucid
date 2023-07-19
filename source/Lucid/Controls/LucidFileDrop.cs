using System.Drawing.Drawing2D;
using Lucid.Common;
using Lucid.Theming;

namespace Lucid.Controls;

public partial class LucidFileDrop : UserControl
{
    private bool _IsDataDraggedOver = false;

    public string DisplayText { get; set; } = "Sample Text";

    public string DisplayTextDragOver { get; set; } = "Sample Drag Over Text";

    public List<string> AllowedFileExtensions { get; set; } = new List<string>();

    #region Events

    /// <summary>
    /// This event fires when files are dropped over the control. Only files with extensions that are contained in <see cref="AllowedFileExtensions"/> are considered.
    /// </summary>
    public event FilesDroppedHandler FilesDropped;

    public delegate void FilesDroppedHandler(List<string> droppedFiles);

    #endregion

    public LucidFileDrop()
    {
        InitializeComponent();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        BackColor = Theming.ThemeProvider.Theme.Colors.MainBackgroundColor;

        var rectPath = Helper.RoundedRectangleHelper.CreateRoundedRectanglePath(new Rectangle(2, 2, Width - 4, Height - 4), 20);

        using (var borderPen = new Pen(ThemeProvider.Theme.Colors.LightText))
        using (var fontBrush = new SolidBrush(ThemeProvider.Theme.Colors.LightText))
        using (var state = new SaveableGraphicsState(e.Graphics))
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            borderPen.DashPattern = new float[] { 5, 2 };

            if (_IsDataDraggedOver)
                borderPen.Width = 2;

            e.Graphics.DrawPath(borderPen, rectPath);
            

            labelDisplayText.Text = _IsDataDraggedOver ? DisplayTextDragOver : DisplayText;
        }
    }

    protected override void OnDragOver(DragEventArgs drgevent)
    {
        base.OnDragOver(drgevent);
        drgevent.Effect = DragDropEffects.Copy;

        _IsDataDraggedOver = true;

        Refresh();
    }
    protected override void OnDragLeave(EventArgs e)
    {
        base.OnDragLeave(e);
        _IsDataDraggedOver = false;

        Refresh();
    }

    protected override void OnDragDrop(DragEventArgs drgevent)
    {
        base.OnDragDrop(drgevent);

        string[] fileList = (string[])drgevent.Data.GetData(DataFormats.FileDrop, false);


        var validFiles = new List<string>();

        foreach (string file in fileList)
        {
            if (AllowedFileExtensions.Any(u => file.EndsWith(u)))
                validFiles.Add(file);
        }

        if (FilesDropped != null)
            FilesDropped(validFiles);

        _IsDataDraggedOver = false;
        Refresh();
    }
}
