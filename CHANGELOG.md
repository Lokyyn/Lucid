# Changelog

All notable changes to this project will be documented in this file.

## [Unreleased]

### Fixed
- `LucidDataGridView`: cell styles were `static readonly` and captured the theme at class-load time — they are now created per-instance in the constructor so theme changes and late theme registration work correctly
- `LucidDataGridView`: alternating row colors were identical (`BackgroundSecondary` for both even and odd) — odd rows now use `BackgroundPrimary` for a visible stripe effect
- `LucidDataGridView`: drag-row indicator used a hardcoded `LightGray` hatch brush — it now draws a solid `Accent`-colored bar and is no longer a static field
- `LucidDataGridView`: right-click context menu showed for `CurrentRow` rather than the row actually clicked — `BaseMouseDown` now selects the hit-tested row before showing the menu
- `LucidDataGridView`: `ContextMenu` property had `[DefaultValue(false)]` — corrected to `[DefaultValue(null)]`
- `LucidDataGridView`: unused `SolidBrush` allocation in `_base_CellPainting` removed

### Added
- `LucidDataGridView`: row hover highlight — hovering over a row shows a `SurfaceDefault` background; only the two affected rows are invalidated per move so performance is unchanged

### Changed
- `LucidDropdownList`: visual style aligned with `LucidComboBox` — flat `BackgroundTertiary` fill, `SurfaceHighlight` border (accent on focus/pressed), shared `scrollbar_arrow_hot` icon
- `LucidDropdownList`: dropdown now uses a custom `ToolStripDropDown` panel with a `LucidScrollBar` instead of `ContextMenuStrip`, enabling proper scrollbar-based scrolling when items exceed `MaxHeight`

### Sample
- Added `LucidDropdownList` to the inputs section in the sample gallery
- Added `LucidDataGridView` to the sample gallery with a controls-overview table demonstrating alternating rows, hover highlight, and the drag indicator

## [2.1.0] - 2026-06-02

### Added
- `LucidTextBox`: `PlaceholderText` property — native cue banner that disappears on focus
- `LucidTextBox`: `ShowClearButton` property — shows a clickable × button when the field contains text
- `LucidTextBox`: `Icon` property — renders an image on the left side of the input

### Fixed
- Restored Accent colors for light and dark theme
- Fix rounded LucidButton invisible until hover due to WS_EX_TRANSPARENT
- Replace LucidComboBox with custom control, remove native scroll arrows

### Changed
- `LucidTextBox`: border now reflects focus (`Accent`), hover (`BorderAccent`), and idle (`BorderDefault`) states consistently
- `LucidTextBox`: fixed DC leak — `ReleaseDC` is now always called after non-client area painting
- `LucidTextBox`: border is only redrawn on `WM_NCPAINT` instead of every Windows message
- `LucidTreeView`: node progress bar now uses theme colors (`SurfaceDefault` for track, `Accent` for fill) instead of hardcoded values
- `LucidPerformanceToolTip`: reworked as a general-purpose themed tooltip — `Difference` is now nullable (`double?`); when not set, renders as a compact themed tooltip without the performance indicator; `SetToolTip(control, text)` works for plain use, `Set(control, text)` for performance mode
- `LucidPerformanceToolTip`: background and text now use `BackgroundSecondary` and `TextPrimary` instead of hardcoded white/black
- `LucidProgressBar`: center-aligned label text now uses `TextPrimary` instead of hardcoded white
- `LucidButton`, `LucidTreeView`: removed unused debug `Color.Red` pens

## [2.0.0] - 2026-05-30

### Added
- Added three new built-in themes: `Dark Green`, `Dark Purple`, and `Light Teal`

### Changed
- Upgraded target framework from .NET 8 to .NET 10
- Revised `Dark` and `Light` theme color values for improved contrast and accessibility (WCAG AA compliance for text)
- Inactive document and tool window tabs now use `TextPrimary` instead of `TextDisabled` for better readability

### Breaking Changes
- All deprecated `Colors` properties (`MainAccent`, `DarkBackground`, `LightText`, etc.) have been fully removed — the semantic tokens introduced in v1.5.0 (`Accent`, `BackgroundPrimary`, `TextPrimary`, …) are now the only supported API
- `Consts2` class (`Theming/Consts.cs`) has been removed — use `ThemeProvider.Theme.Sizes` instead


## [1.5.0] - 2026-05-21

### Deprecated
- `Colors.MainAccent` → use `Colors.Accent`
- `Colors.DarkBackground` / `Colors.MediumBackground` → use `Colors.BackgroundPrimary`
- `Colors.MainBackgroundColor` / `Colors.HeaderBackground` / `Colors.RowOdd` → use `Colors.BackgroundSecondary`
- `Colors.LightBackground` / `Colors.DockBackground` / `Colors.RowEven` → use `Colors.BackgroundTertiary`
- `Colors.LighterBackground` / `Colors.GreyHighlight` / `Colors.DockInactive` / `Colors.InactivScrollbar` → use `Colors.SurfaceDefault`
- `Colors.LightestBackground` / `Colors.GreySelection` / `Colors.DarkGreySelection` / `Colors.HotScrollbar` / `Colors.ActiveControl` → use `Colors.SurfaceHighlight`
- `Colors.LightBorder` / `Colors.DarkBorder` → use `Colors.BorderDefault`
- `Colors.DarkBlueBorder` / `Colors.LightBlueBorder` → use `Colors.BorderAccent`
- `Colors.BlueBackground` / `Colors.DockMovedHighlight` / `Colors.DockActive` / `Colors.ControlHighlight` / `Colors.LabelLinkAccent` → use `Colors.Accent`
- `Colors.DarkBlueBackground` / `Colors.LabelLinkHoveredAccent` → use `Colors.AccentSecondary`
- `Colors.LightText` → use `Colors.TextPrimary`
- `Colors.DisabledText` → use `Colors.TextDisabled`

## [1.4.0] - 2026-05-14

### Added
- Added `LucidSlider` component

### Fixed
- Fixed TreeView-Node Progressbar

## [1.3.0] - 2026-05-09

### Added
- XML documentation for `LucidChipControl`, `LucidFileDrop`, `LucidProgressBar`, `LucidTreeNode`, `LucidTreeView`, `LucidDockContent`, `LucidDockPanel`, `LucidDocument`, `LucidToolWindow`, and `ThemeProvider`
- Extended sample gallery with additional controls on the main page

### Fixed
- Various fixes to `LucidScrollableControl` (layout, sizing, scroll behaviour)

### Changed
- Refactored and expanded README with more detailed documentation

## [1.2.0] - 2026-04-30

### Added
- CODEOWNERS file for repository maintainers

### Changed
- Improved `LucidProgressBar` rendering and sample gallery

## [1.1.0] - 2024-08-13

### Changed
- Upgraded target framework from .NET 6 to .NET 8
- Updated CI/CD build and publish workflows for .NET 8

## [1.0] - 2023-07-17 → 2024-04-27

Covers patch releases 1.0.2 through 1.0.13.

### Added
- Initial set of dark-themed WinForms controls: `LucidButton`, `LucidCheckBox`, `LucidComboBox`, `LucidLabel`,
  `LucidTextBox`, `LucidTreeView`, `LucidProgressBar`, `LucidMessageBox`, `LucidChipControl`, and more
- `LucidProgressBar`: option to display the current percentage as text inside the control
- `LucidChipControl`: chips can be deleted by the user
- `LucidTreeView`: rounded rectangle style for selected nodes
- `ThemeProvider`: support for user-registered custom themes
- Base theme properties can be set from outside the assembly
- XML documentation for all theming types
- Sample gallery with message box, file drop, and progress bar percentage examples
- Getting-started documentation moved to a dedicated folder structure
- NuGet package metadata (license, description, readme) and automated publish workflow

### Fixed
- Spelling error in `LucidTextBox` control name
- Bug when activating custom registered themes

### Changed
- All controls renamed to use the `Lucid` prefix consistently
- Namespaces reorganized using file-scoped namespace declarations
- Removed unused `using` directives across the codebase
- Sample gallery structure reorganized
