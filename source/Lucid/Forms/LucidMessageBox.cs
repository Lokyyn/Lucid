using Lucid.Icons;
using System.ComponentModel;
using System.Globalization;

namespace Lucid.Forms;

public partial class LucidMessageBox : LucidDialog
{
    #region Field Region

    private string _message;
    private int _maximumWidth = 350;

    #endregion

    #region Property Region

    [Description("Determines the maximum width of the message box when it autosizes around the displayed message.")]
    [DefaultValue(350)]
    public int MaximumWidth
    {
        get { return _maximumWidth; }
        set
        {
            _maximumWidth = value;
            CalculateSize();
        }
    }

    #endregion

    #region Constructor Region

    public LucidMessageBox()
    {
        InitializeComponent();
    }

    public LucidMessageBox(string message, string title, LucidMessageBoxIcon icon, LucidDialogButton buttons, CultureInfo culture = null)
        : this()
    {
        Text = title;
        _message = message;

        Translate(culture);

        DialogButtons = buttons;
        SetIcon(icon);
    }

    public LucidMessageBox(string message)
        : this(message, null, LucidMessageBoxIcon.None, LucidDialogButton.Ok)
    { }

    public LucidMessageBox(string message, string title)
        : this(message, title, LucidMessageBoxIcon.None, LucidDialogButton.Ok)
    { }

    public LucidMessageBox(string message, string title, LucidDialogButton buttons)
        : this(message, title, LucidMessageBoxIcon.None, buttons)
    { }

    public LucidMessageBox(string message, string title, LucidMessageBoxIcon icon)
        : this(message, title, icon, LucidDialogButton.Ok)
    { }

    #endregion

    #region Static Method Region

    public static DialogResult ShowInformation(string message, string caption, LucidDialogButton buttons = LucidDialogButton.Ok, CultureInfo culture = null)
    {
        return ShowDialog(message, caption, LucidMessageBoxIcon.Information, buttons, culture);
    }

    public static DialogResult ShowWarning(string message, string caption, LucidDialogButton buttons = LucidDialogButton.Ok, CultureInfo culture = null)
    {
        return ShowDialog(message, caption, LucidMessageBoxIcon.Warning, buttons, culture);
    }

    public static DialogResult ShowError(string message, string caption, LucidDialogButton buttons = LucidDialogButton.Ok, CultureInfo culture = null)
    {
        return ShowDialog(message, caption, LucidMessageBoxIcon.Error, buttons, culture);
    }

    private static DialogResult ShowDialog(string message, string caption, LucidMessageBoxIcon icon, LucidDialogButton buttons, CultureInfo culture = null)
    {
        using (var dlg = new LucidMessageBox(message, caption, icon, buttons, culture))
        {
            var result = dlg.ShowDialog();
            return result;
        }
    }

    #endregion

    #region Method Region

    private void SetIcon(LucidMessageBoxIcon icon)
    {
        switch (icon)
        {
            case LucidMessageBoxIcon.None:
                picIcon.Visible = false;
                lblText.Left = 10;
                break;
            case LucidMessageBoxIcon.Information:
                picIcon.Image = MessageBoxIcons.info;
                break;
            case LucidMessageBoxIcon.Warning:
                picIcon.Image = MessageBoxIcons.warning;
                break;
            case LucidMessageBoxIcon.Error:
                picIcon.Image = MessageBoxIcons.error;
                break;
        }
    }

    private void CalculateSize()
    {
        var width = 260; var height = 124;

        // Reset form back to original size
        Size = new Size(width, height);

        lblText.Text = string.Empty;
        lblText.AutoSize = true;
        lblText.Text = _message;

        // Set the minimum dialog size to whichever is bigger - the original size or the buttons.
        var minWidth = Math.Max(width, TotalButtonSize + 15);

        var titleSize = TextRenderer.MeasureText(this.Text, lblText.Font); // Form title size

        // Calculate the total size of the message
        var totalWidth = lblText.Right + 25 + (Math.Abs(titleSize.Width - Size.Width));

        // Make sure we're not making the dialog bigger than the maximum size
        if (totalWidth < _maximumWidth)
        {
            // Width is smaller than the maximum width.
            // This means we can have a single-line message box.
            // Move the label to accomodate this.
            width = totalWidth;
            lblText.Top = picIcon.Top + (picIcon.Height / 2) - (lblText.Height / 2);
        }
        else
        {
            // Width is larger than the maximum width.
            // Change the label size and wrap it.
            width = _maximumWidth;
            var offsetHeight = Height - picIcon.Height;
            lblText.AutoUpdateHeight = true;
            lblText.Width = width - lblText.Left - 25;
            height = offsetHeight + lblText.Height;
        }

        // Force the width to the minimum width
        if (width < minWidth)
            width = minWidth;

        // Set the new size of the dialog
        Size = new Size(width, height);
    }

    #endregion

    #region Event Handler Region

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        CalculateSize();
    }

    #endregion
}
