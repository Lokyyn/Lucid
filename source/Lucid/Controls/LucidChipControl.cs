using System.Data;
using System.Drawing.Drawing2D;
using Lucid.Theming;

namespace Lucid.Controls;

public partial class LucidChipControl : UserControl
{
    #region Privates
    private Font _BadgeFont = new Font("Tahoma Sans Serif", 10f, FontStyle.Bold);
    private int _ScrolloffsetY;
    private int _BadgeSpacing = 5;
    private int _LineSpacing = 25;

    private Rectangle _DrawingArea;
    private List<Rectangle> _BadgeRectangleCache;
    #endregion


    #region Public properties

    /// <summary>
    /// The datasource for displaying chips
    /// </summary>
    public List<Chip> Chips;

    /// <summary>
    /// Defines the symmetric padding inwards of the control. The Chips are rendered with that space between them and the bounds.
    /// </summary>
    public int SymmetricPadding { get; set; } = 10;

    /// <summary>
    /// Defines if chips can be selected.
    /// </summary>
    public bool AllowChipSelection { get; set; } = true;

    /// <summary>
    /// Defines if chips can be deleted
    /// </summary>
    public bool AllowChipDeletion { get; set; } = false;

    /// <summary>
    /// Defines if the badge color should be changed when the cursor moves above an chip.
    /// </summary>
    public bool HighlightChipUnderCursor { get; set; } = true;

    /// <summary>
    /// Defines if the chips in general are enabled. If <see cref="false"/> the chips selection and highlight color are not active.
    /// </summary>
    public bool ChipsEnabled { get; set; } = true;

    /// <summary>
    /// Defines the selection mode. Either one chip at a time is selectable or all of them.
    /// </summary>
    public SelectionMode SelectionMode { get; set; } = SelectionMode.Multiple;

    /// <summary>
    /// Indicates if the control has chips that are displayed
    /// </summary>
    public bool HasChips => Chips != null && Chips.Count > 0;

    // Defines the minimum width at which the chip controls has a horizontal scrollbar
    //public int MinimumControlWidth { get; set; } = 40;

    #endregion


    public LucidChipControl()
    {
        InitializeComponent();
        DoubleBuffered = true;
        BackColor = ThemeProvider.Theme.Colors.MainBackgroundColor;
        Chips = new List<Chip>();

        VScrollBar.ValueChanged += VScrollBar_ValueChanged;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Calculation

        // Asymmetric padding example
        //_DrawingArea = new Rectangle(Padding.Top, Padding.Left, Bounds.Width - (Padding.Left + Padding.Right), Bounds.Height - (Padding.Top + Padding.Bottom));

        _DrawingArea = new Rectangle(SymmetricPadding, SymmetricPadding, Bounds.Width - (SymmetricPadding + SymmetricPadding) - VScrollBar.Width, Bounds.Height - (SymmetricPadding + SymmetricPadding));


        //e.Graphics.DrawRectangle(new Pen(Color.Red), _DrawingArea); // Drawing Area (DEBUG)


        BackColor = Theming.ThemeProvider.Theme.Colors.MainBackgroundColor;

        if (Chips != null)
        {
            _BadgeRectangleCache = new List<Rectangle>();

            using (Common.SaveableGraphicsState state = new Common.SaveableGraphicsState(e.Graphics))
            {
                // Set the initial starting location to the top left corner of the Drawing Area
                var currentXPosition = _DrawingArea.X;
                var currentYPosition = _DrawingArea.Y - VScrollBar.Value;

                // Iterate through all badges
                foreach (var current in Chips)
                {
                    // We do not need to draw badges that are now visible or having no text
                    if (!current.Visible || string.IsNullOrEmpty(current.Text))
                        continue;

                    // Determine the starting location to draw the badge
                    var xCord = Math.Max(currentXPosition, _DrawingArea.X); // We need the X Cordinate that is bigger than the X bounds of the drawing 
                    var yCord = Math.Max(currentYPosition, _DrawingArea.Y - VScrollBar.Value); // We need the Y Cordinate that is bigger than the Y bounds of the drawing 

                    // Now we measure the text in order to determine the badge width
                    var textSize = e.Graphics.MeasureString(current.Text, _BadgeFont);
                    textSize.Width = textSize.Width + 10;

                    // Calculate the size of the badge
                    var badgeRect = new Rectangle(xCord, yCord, (int)textSize.Width, 20);

                    if (badgeRect.Width > _DrawingArea.Width) // Does the badge fit in the drawing area?
                        continue;
                    else if (currentXPosition + badgeRect.Width > _DrawingArea.Width) // Badge doesn't fit in current line -> start new line
                    {
                        currentYPosition = currentYPosition + _LineSpacing;
                        currentXPosition = _DrawingArea.X;
                        badgeRect = new Rectangle(currentXPosition, currentYPosition, (int)textSize.Width, 20);
                    }

                    // Check colors or use the default ones
                    var badgeBackColor = current.BackColor == Color.Empty ? ColorTranslator.FromHtml("#ebebeb") : current.BackColor;
                    var badgeForeColor = current.BackColor == Color.Empty ? ColorTranslator.FromHtml("#000000") : Helper.ColorExtender.GetContrastColorBW(badgeBackColor);


                    // ######## Drawing ##########

                    // If the color is hot we need to change its backcolor to an darker color
                    if (current.IsHot && HighlightChipUnderCursor)
                        badgeBackColor = ControlPaint.Dark(badgeBackColor, 0.05f);

                    // It its selected we need to increase the rectangles size
                    if (current.IsSelected)
                        badgeRect.Width += Icons.ControlIcons.icon_checked_white.Width;

                    var badgePath = Helper.RoundedRectangleHelper.CreateRoundedRectanglePath(badgeRect, 12); // Create the badge rectangle path

                    if (badgeRect.Bottom > 0 && badgeRect.Top < Bounds.Height)
                        DrawBadge(e.Graphics, badgePath, badgeBackColor, badgeForeColor, current, new Point(badgeRect.X + 5, badgeRect.Y + 2), new Point(badgeRect.Right - 5, badgeRect.Bottom));

                    // ######## Drawing ##########


                    // Update the X-Position with the current badge width and spacing
                    currentXPosition = currentXPosition + badgeRect.Width + _BadgeSpacing;

                    // Add the badge rectangle to cache
                    _BadgeRectangleCache.Add(badgeRect);
                    current.BadgeRectangle = badgeRect;
                }
            }

            UpdateScrollbar();
        }
    }

    /// <summary>
    /// Draws an badge with the given parameters
    /// </summary>
    /// <param name="g"></param>
    /// <param name="badgePath"></param>
    /// <param name="backColor"></param>
    /// <param name="foreColor"></param>
    /// <param name="text"></param>
    /// <param name="startingPoint"></param>
    private void DrawBadge(Graphics g, GraphicsPath badgePath, Color backColor, Color foreColor, Chip chip, Point startingPoint, Point endingPoint)
    {
        g.CompositingQuality = CompositingQuality.HighQuality;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.SmoothingMode = SmoothingMode.HighQuality;

        // Draw the badge
        using (var backBrush = new SolidBrush(backColor))
        using (var foreBrush = new SolidBrush(foreColor))
        using (var borderPen = new Pen(ThemeProvider.Theme.Colors.LightText))
        {
            // Gradient badges
            if (chip.ShowGradientIfAvailable && chip.BackColor2 != Color.Empty)
            {
                using var pg = new PathGradientBrush(badgePath);

                var badgeBackColor2 = chip.BackColor2 == Color.Empty ? ColorTranslator.FromHtml("#ebebeb") : chip.BackColor2;

                pg.CenterColor = chip.BackColor;
                pg.CenterPoint = new PointF(startingPoint.X, startingPoint.Y);
                pg.SurroundColors = new Color[] { badgeBackColor2 };

                g.FillPath(pg, badgePath);
                g.DrawPath(borderPen, badgePath);
            }
            else
            {
                g.FillPath(backBrush, badgePath);
                g.DrawPath(borderPen, badgePath);
            }


            if (chip.IsSelected)
            {
                var icon = ColorTranslator.ToHtml(foreColor) == "White" ? Icons.ControlIcons.icon_checked_white : Icons.ControlIcons.icon_checked_black;

                g.DrawImage(icon, startingPoint);
                g.DrawString(chip.Text, _BadgeFont, foreBrush, new Point(startingPoint.X + Icons.ControlIcons.icon_checked_white.Width, startingPoint.Y));
            }
            else
            {
                if (chip.IsHot && AllowChipDeletion)
                {
                    var deleteIcon = ColorTranslator.ToHtml(foreColor) == "White" ? DockIcons.close_white_unselected_12 : DockIcons.close_dark_unselected_12;
                    var height = endingPoint.Y - startingPoint.Y;
                    var imageDrawPoint = new Point(endingPoint.X - deleteIcon.Width + 2, endingPoint.Y - height + 2);

                    chip.DeleteRectangle = new Rectangle(imageDrawPoint, new Size(deleteIcon.Width, deleteIcon.Height));

                    g.DrawImage(deleteIcon, imageDrawPoint);
                    g.DrawString(chip.Text.Substring(0, Math.Max(0, chip.Text.Length - 3)) + "…", _BadgeFont, foreBrush, startingPoint);

                }
                else
                    g.DrawString(chip.Text, _BadgeFont, foreBrush, startingPoint);
            }
        }

    }

    #region Custom events

    /// <summary>
    /// This event fires when an chip is selected
    /// </summary>
    public event OnChipSelectedHandler OnChipSelected;

    public delegate void OnChipSelectedHandler(Chip newSelectedChip, List<Chip> allSelectedChips);

    /// <summary>
    /// This event fires when an chip selection is changed
    /// </summary>
    public event OnChipSelectionChangedHandler OnChipSelectionChanged;

    public delegate void OnChipSelectionChangedHandler(Chip newSelectedChip, List<Chip> allSelectedChips);

    /// <summary>
    /// This event fires when an chip is deleted by clicking on it
    /// </summary>
    public event OnChipDeletedHandler OnChipDeleted;

    public delegate void OnChipDeletedHandler(Chip deletedChip);

    #endregion


    #region Scrolling
    protected override void OnMouseWheel(MouseEventArgs e)
    {
        if (VScrollBar.Visible)
        {
            int real = e.Delta / 15;

            if (real < 0)
                VScrollBar.Value += Math.Abs(real);
            else
                VScrollBar.Value -= Math.Abs(real);
        }

        base.OnMouseWheel(e);
    }
    private void UpdateScrollbar()
    {
        if (_BadgeRectangleCache == null || _BadgeRectangleCache.Count == 0)
        {
            VScrollBar.Visible = false;
            return;
        }

        var maxPoint = _BadgeRectangleCache.Max(u => u.Bottom) + _ScrolloffsetY;

        if (maxPoint <= _DrawingArea.Height)
        {
            VScrollBar.Visible = false;
            VScrollBar.Value = 0;
            _ScrolloffsetY = 0;
            return;
        }

        if (maxPoint > _DrawingArea.Bottom) // Indicates that badges where drawn outside the Drawing Area -> Show vertical scrollbar
        {
            VScrollBar.Visible = true;
            VScrollBar.Enabled = true;
            VScrollBar.Maximum = maxPoint; // - _DrawingArea.Height + (3 * SymmetricPadding);
            VScrollBar.ViewSize = _DrawingArea.Height;
        }
    }
    private void VScrollBar_ValueChanged(object sender, ScrollValueEventArgs e)
    {
        _ScrolloffsetY = VScrollBar.Value;
        Invalidate();
    }

    #endregion

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        Invalidate();

        UpdateScrollbar();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        if (_BadgeRectangleCache == null || _BadgeRectangleCache.Count == 0)
            return;

        var badgeHit = Chips.FirstOrDefault(u => u.BadgeRectangle.Contains(e.Location));

        if (badgeHit != null && badgeHit.BadgeRectangle != Rectangle.Empty && ChipsEnabled)
        {
            badgeHit.IsHot = true;
        }
        else
        {
            Chips.ForEach(u => u.IsHot = false);
        }

        Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);

        if (_BadgeRectangleCache == null || _BadgeRectangleCache.Count == 0 || !AllowChipSelection)
            return;

        var chipHit = Chips.FirstOrDefault(u => u.BadgeRectangle.Contains(e.Location));

        if (chipHit != null && chipHit.BadgeRectangle != Rectangle.Empty && ChipsEnabled)
        {
            if (AllowChipDeletion && chipHit.DeleteRectangle.Contains(e.Location))
            {
                Chips.Remove(chipHit);

                // Trigger event
                if (OnChipDeleted != null)
                    OnChipDeleted(chipHit);
            }
            else if (SelectionMode == SelectionMode.Multiple)
            {
                chipHit.IsSelected = !chipHit.IsSelected;

                // Trigger events 
                if (OnChipSelected != null && chipHit.IsSelected)
                    OnChipSelectionChanged(chipHit, Chips.Where(u => u.IsSelected).ToList());

                if (OnChipSelectionChanged != null)
                    OnChipSelectionChanged(chipHit, Chips.Where(u => u.IsSelected).ToList());
            }
            else if (SelectionMode == SelectionMode.Single)
            {
                var newSelection = !chipHit.IsSelected;

                Chips.ForEach(u => u.IsSelected = false);
                chipHit.IsSelected = newSelection;


                // Trigger events 
                if (OnChipSelected != null && chipHit.IsSelected)
                    OnChipSelected(chipHit, Chips.Where(u => u.IsSelected).ToList());

                if (OnChipSelectionChanged != null)
                    OnChipSelected(chipHit, Chips.Where(u => u.IsSelected).ToList());
            }

            Invalidate();
        }
    }
}

public class Chip
{
    public bool IsSelected { get; internal set; } = false;

    internal bool IsHot { get; set; } = false;

    internal Rectangle BadgeRectangle { get; set; } = Rectangle.Empty;

    internal Rectangle DeleteRectangle { get; set; } = Rectangle.Empty;

    public string Text { get; set; }

    public Color BackColor { get; set; }

    public Color BackColor2 { get; set; }

    public bool Visible { get; set; } = true;

    public bool ShowGradientIfAvailable { get; set; } = false;

    public object Tag { get; set; }
}

public enum SelectionMode
{
    Single,
    Multiple
}
