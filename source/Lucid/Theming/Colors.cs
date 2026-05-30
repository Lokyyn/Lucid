namespace Lucid.Theming;

public record class Colors
{
    // Backgrounds

    /// <summary>
    /// The deepest background layer (e.g. outer shell, sidebar base, dark panels).
    /// </summary>
    /// <remarks>Replaces: DarkBackground, MediumBackground</remarks>
    public Color BackgroundPrimary { get; init; }

    /// <summary>
    /// The standard background for most controls, forms and dock panels.
    /// </summary>
    /// <remarks>Replaces: MainBackgroundColor, HeaderBackground, RowOdd</remarks>
    public Color BackgroundSecondary { get; init; }

    /// <summary>
    /// A slightly elevated background for surfaces, cards and alternate rows.
    /// </summary>
    /// <remarks>Replaces: LightBackground, DockBackground, RowEven</remarks>
    public Color BackgroundTertiary { get; init; }

    // Surface / Interaction States

    /// <summary>
    /// Default surface color for hovered or mildly highlighted areas
    /// (e.g. menu hover, scrollbar track, inactive dock header).
    /// </summary>
    /// <remarks>Replaces: LighterBackground, GreyHighlight, DockInactive, InactivScrollbar</remarks>
    public Color SurfaceDefault { get; init; }

    /// <summary>
    /// Stronger highlight for selected or active surface areas
    /// (e.g. selected list item, pressed button bg, scrollbar thumb hover).
    /// </summary>
    /// <remarks>Replaces: LightestBackground, GreySelection, DarkGreySelection, HotScrollbar, ActiveControl</remarks>
    public Color SurfaceHighlight { get; init; }

    // Borders

    /// <summary>
    /// Neutral border for separators, panel edges and control outlines.
    /// </summary>
    /// <remarks>Replaces: LightBorder, DarkBorder</remarks>
    public Color BorderDefault { get; init; }

    /// <summary>
    /// Accent-tinted border used around focused or active controls.
    /// </summary>
    /// <remarks>Replaces: DarkBlueBorder, LightBlueBorder</remarks>
    public Color BorderAccent { get; init; }

    // Accent / Brand

    /// <summary>
    /// Primary accent / brand color used for active selections, highlights,
    /// focused controls, dock move indicator and link labels.
    /// </summary>
    /// <remarks>Replaces: MainAccent, BlueBackground, DockMovedHighlight, DockActive,
    /// ControlHighlight, LabelLinkAccent</remarks>
    public Color Accent { get; init; }

    /// <summary>
    /// Softer / secondary accent for hover states on accent elements
    /// (e.g. hovered link labels, secondary badge).
    /// </summary>
    /// <remarks>Replaces: DarkBlueBackground, LabelLinkHoveredAccent</remarks>
    public Color AccentSecondary { get; init; }

    // Text

    /// <summary>
    /// Main foreground / text color for all readable content.
    /// </summary>
    /// <remarks>Replaces: LightText</remarks>
    public Color TextPrimary { get; init; }

    /// <summary>
    /// Muted text color for disabled labels, placeholders and hints.
    /// </summary>
    /// <remarks>Replaces: DisabledText</remarks>
    public Color TextDisabled { get; init; }
}
