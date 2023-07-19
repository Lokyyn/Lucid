using Lucid.Controls;
using System.ComponentModel;
using System.Globalization;

namespace Lucid.Forms;

public partial class LucidDialog : LucidForm
{
    #region Field Region

    private LucidDialogButton _dialogButtons = LucidDialogButton.Ok;
    private List<LucidButton> _buttons;

    #endregion

    #region Button Region

    protected LucidButton btnOk;
    protected LucidButton btnCancel;
    protected LucidButton btnClose;
    protected LucidButton btnYes;
    protected LucidButton btnNo;
    protected LucidButton btnAbort;
    protected LucidButton btnRetry;
    protected LucidButton btnIgnore;

    #endregion

    #region Property Region

    [Description("Determines the type of the dialog window.")]
    [DefaultValue(LucidDialogButton.Ok)]
    public LucidDialogButton DialogButtons
    {
        get { return _dialogButtons; }
        set
        {
            if (_dialogButtons == value)
                return;

            _dialogButtons = value;
            SetButtons();
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int TotalButtonSize { get; private set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new IButtonControl AcceptButton
    {
        get { return base.AcceptButton; }
        private set { base.AcceptButton = value; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new IButtonControl CancelButton
    {
        get { return base.CancelButton; }
        private set { base.CancelButton = value; }
    }

    #endregion

    #region Constructor Region

    public LucidDialog()
    {
        InitializeComponent();
        Translate();

        _buttons = new List<LucidButton>
            {
                btnAbort, btnRetry, btnIgnore, btnOk,
                btnCancel, btnClose, btnYes, btnNo
            };
    }

    #endregion

    #region Event Handler Region

    protected override void OnLoad(System.EventArgs e)
    {
        base.OnLoad(e);

        SetButtons();
    }

    #endregion

    #region Method Region

    private void SetButtons()
    {
        foreach (var btn in _buttons)
            btn.Visible = false;

        switch (_dialogButtons)
        {
            case LucidDialogButton.Ok:
                ShowButton(btnOk, true);
                AcceptButton = btnOk;
                break;
            case LucidDialogButton.Close:
                ShowButton(btnClose, true);
                AcceptButton = btnClose;
                CancelButton = btnClose;
                break;
            case LucidDialogButton.OkCancel:
                ShowButton(btnOk);
                ShowButton(btnCancel, true);
                AcceptButton = btnOk;
                CancelButton = btnCancel;
                break;
            case LucidDialogButton.AbortRetryIgnore:
                ShowButton(btnAbort);
                ShowButton(btnRetry);
                ShowButton(btnIgnore, true);
                AcceptButton = btnAbort;
                CancelButton = btnIgnore;
                break;
            case LucidDialogButton.RetryCancel:
                ShowButton(btnRetry);
                ShowButton(btnCancel, true);
                AcceptButton = btnRetry;
                CancelButton = btnCancel;
                break;
            case LucidDialogButton.YesNo:
                ShowButton(btnYes);
                ShowButton(btnNo, true);
                AcceptButton = btnYes;
                CancelButton = btnNo;
                break;
            case LucidDialogButton.YesNoCancel:
                ShowButton(btnYes);
                ShowButton(btnNo);
                ShowButton(btnCancel, true);
                AcceptButton = btnYes;
                CancelButton = btnCancel;
                break;
        }

        SetFlowSize();
    }

    private void ShowButton(LucidButton button, bool isLast = false)
    {
        button.SendToBack();

        if (!isLast)
            button.Margin = new Padding(0, 0, 10, 0);

        button.Visible = true;
    }

    private void SetFlowSize()
    {
        var width = flowInner.Padding.Horizontal;

        foreach (var btn in _buttons)
        {
            if (btn.Visible)
                width += btn.Width + btn.Margin.Right;
        }

        flowInner.Width = width;
        TotalButtonSize = width;
    }

    internal void Translate(CultureInfo culture = null)
    {
        btnOk.Text = Localization.Localizer.GetString("Dialog.Ok", culture);
        btnCancel.Text = Localization.Localizer.GetString("Dialog.Cancel", culture);
        btnClose.Text = Localization.Localizer.GetString("Dialog.Close", culture);
        btnIgnore.Text = Localization.Localizer.GetString("Dialog.Ignore", culture);
        btnNo.Text = Localization.Localizer.GetString("Dialog.No", culture);
        btnAbort.Text = Localization.Localizer.GetString("Dialog.Abort", culture);
        btnYes.Text = Localization.Localizer.GetString("Dialog.Yes", culture);
        btnRetry.Text = Localization.Localizer.GetString("Dialog.Retry", culture);
    }

    #endregion
}
