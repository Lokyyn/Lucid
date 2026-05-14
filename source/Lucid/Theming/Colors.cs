namespace Lucid.Theming;

public class Colors
{
    // Backgrounds

    /// <summary>
    /// The deepest background layer (e.g. outer shell, sidebar base, dark panels).
    /// </summary>
    public Color BackgroundPrimary { get; set; }

    /// <summary>
    /// The standard background for most controls, forms and dock panels.
    /// </summary>
    public Color BackgroundSecondary { get; set; }

    /// <summary>
    /// A slightly elevated background for surfaces, cards and alternate rows.
    /// </summary>
    public Color BackgroundTertiary { get; set; }

    // Surface / Interaction States

    /// <summary>
    /// Default surface color for hovered or mildly highlighted areas
    /// (e.g. menu hover, scrollbar track, inactive dock header).
    /// </summary>
    public Color SurfaceDefault { get; set; }

    /// <summary>
    /// Stronger highlight for selected or active surface areas
    /// (e.g. selected list item, pressed button bg, scrollbar thumb hover).
    /// </summary>
    public Color SurfaceHighlight { get; set; }

    // Borders

    /// <summary>
    /// Neutral border for separators, panel edges and control outlines.
    /// </summary>
    public Color BorderDefault { get; set; }

    /// <summary>
    /// Accent-tinted border used around focused or active controls.
    /// </summary>
    public Color BorderAccent { get; set; }

    // Accent / Brand

    /// <summary>
    /// Primary accent / brand color used for active selections, highlights,
    /// focused controls, dock move indicator and link labels.
    /// </summary>
    public Color Accent { get; set; }

    /// <summary>
    /// Softer / secondary accent for hover states on accent elements
    /// (e.g. hovered link labels, secondary badge).
    /// </summary>
    public Color AccentSecondary { get; set; }

    // Text

    /// <summary>
    /// Main foreground / text color for all readable content.
    /// </summary>
    public Color TextPrimary { get; set; }

    /// <summary>
    /// Muted text color for disabled labels, placeholders and hints.
    /// </summary>
    public Color TextDisabled { get; set; }

    // Obsolete — delegate to new properties

    [Obsolete("Use BackgroundPrimary instead. This property will be removed in a future release.")]
    public Color DarkBackground { get => BackgroundPrimary; set => BackgroundPrimary = value; }

    [Obsolete("Use BackgroundPrimary instead. This property will be removed in a future release.")]
    public Color MediumBackground { get => BackgroundPrimary; set => BackgroundPrimary = value; }

    [Obsolete("Use BackgroundSecondary instead. This property will be removed in a future release.")]
    public Color MainBackgroundColor { get => BackgroundSecondary; set => BackgroundSecondary = value; }

    [Obsolete("Use BackgroundSecondary instead. This property will be removed in a future release.")]
    public Color HeaderBackground { get => BackgroundSecondary; set => BackgroundSecondary = value; }

    [Obsolete("Use BackgroundSecondary instead. This property will be removed in a future release.")]
    public Color RowOdd { get => BackgroundSecondary; set => BackgroundSecondary = value; }

    [Obsolete("Use BackgroundTertiary instead. This property will be removed in a future release.")]
    public Color DockBackground { get => BackgroundTertiary; set => BackgroundTertiary = value; }

    [Obsolete("Use BackgroundTertiary instead. This property will be removed in a future release.")]
    public Color LightBackground { get => BackgroundTertiary; set => BackgroundTertiary = value; }

    [Obsolete("Use BackgroundTertiary instead. This property will be removed in a future release.")]
    public Color RowEven { get => BackgroundTertiary; set => BackgroundTertiary = value; }

    [Obsolete("Use SurfaceDefault instead. This property will be removed in a future release.")]
    public Color LighterBackground { get => SurfaceDefault; set => SurfaceDefault = value; }

    [Obsolete("Use SurfaceDefault instead. This property will be removed in a future release.")]
    public Color GreyHighlight { get => SurfaceDefault; set => SurfaceDefault = value; }

    [Obsolete("Use SurfaceDefault instead. This property will be removed in a future release.")]
    public Color DockInactive { get => SurfaceDefault; set => SurfaceDefault = value; }

    [Obsolete("Use SurfaceDefault instead. This property will be removed in a future release.")]
    public Color InactivScrollbar { get => SurfaceDefault; set => SurfaceDefault = value; }

    [Obsolete("Use SurfaceHighlight instead. This property will be removed in a future release.")]
    public Color LightestBackground { get => SurfaceHighlight; set => SurfaceHighlight = value; }

    [Obsolete("Use SurfaceHighlight instead. This property will be removed in a future release.")]
    public Color GreySelection { get => SurfaceHighlight; set => SurfaceHighlight = value; }

    [Obsolete("Use SurfaceHighlight instead. This property will be removed in a future release.")]
    public Color DarkGreySelection { get => SurfaceHighlight; set => SurfaceHighlight = value; }

    [Obsolete("Use SurfaceHighlight instead. This property will be removed in a future release.")]
    public Color HotScrollbar { get => SurfaceHighlight; set => SurfaceHighlight = value; }

    [Obsolete("Use SurfaceHighlight instead. This property will be removed in a future release.")]
    public Color ActiveControl { get => SurfaceHighlight; set => SurfaceHighlight = value; }

    [Obsolete("Use BorderDefault instead. This property will be removed in a future release.")]
    public Color LightBorder { get => BorderDefault; set => BorderDefault = value; }

    [Obsolete("Use BorderDefault instead. This property will be removed in a future release.")]
    public Color DarkBorder { get => BorderDefault; set => BorderDefault = value; }

    [Obsolete("Use BorderAccent instead. This property will be removed in a future release.")]
    public Color DarkBlueBorder { get => BorderAccent; set => BorderAccent = value; }

    [Obsolete("Use BorderAccent instead. This property will be removed in a future release.")]
    public Color LightBlueBorder { get => BorderAccent; set => BorderAccent = value; }

    [Obsolete("Use Accent instead. This property will be removed in a future release.")]
    public Color MainAccent { get => Accent; set => Accent = value; }

    [Obsolete("Use Accent instead. This property will be removed in a future release.")]
    public Color BlueBackground { get => Accent; set => Accent = value; }

    [Obsolete("Use Accent instead. This property will be removed in a future release.")]
    public Color DockMovedHighlight { get => Accent; set => Accent = value; }

    [Obsolete("Use Accent instead. This property will be removed in a future release.")]
    public Color DockActive { get => Accent; set => Accent = value; }

    [Obsolete("Use Accent instead. This property will be removed in a future release.")]
    public Color ControlHighlight { get => Accent; set => Accent = value; }

    [Obsolete("Use Accent instead. This property will be removed in a future release.")]
    public Color LabelLinkAccent { get => Accent; set => Accent = value; }

    [Obsolete("Use AccentSecondary instead. This property will be removed in a future release.")]
    public Color DarkBlueBackground { get => AccentSecondary; set => AccentSecondary = value; }

    [Obsolete("Use AccentSecondary instead. This property will be removed in a future release.")]
    public Color LabelLinkHoveredAccent { get => AccentSecondary; set => AccentSecondary = value; }

    [Obsolete("Use TextPrimary instead. This property will be removed in a future release.")]
    public Color LightText { get => TextPrimary; set => TextPrimary = value; }

    [Obsolete("Use TextDisabled instead. This property will be removed in a future release.")]
    public Color DisabledText { get => TextDisabled; set => TextDisabled = value; }
}
