# Changelog

All notable changes to this project will be documented in this file.

## [Unreleased]

### Added
- Added three new built-in themes: `Dark Green`, `Dark Purple`, and `Light Teal`

### Changed
- Introduced new semantic color tokens (`BackgroundPrimary`, `BackgroundSecondary`, `BackgroundTertiary`, `SurfaceDefault`, `SurfaceHighlight`, `BorderDefault`, `BorderAccent`, `Accent`, `AccentSecondary`, `TextPrimary`, `TextDisabled`) on the `Colors` class
- All old color properties (e.g. `MainAccent`, `LightText`, `DarkBackground`, `GreySelection`) are now marked `[Obsolete]` and delegate to the new tokens — existing code continues to compile with a warning
- Updated `BaseDarkTheme` and `BaseLightTheme` to set the new color tokens directly
- All internal controls and renderers migrated to the new color tokens
- Revised `Dark` and `Light` theme color values for improved contrast and accessibility (WCAG AA compliance for text)
- Inactive document and tool window tabs now use `TextPrimary` instead of `TextDisabled` for better readability

### Breaking Changes
- `Colors` is now a `record class` — individual color properties are `init`-only and can no longer be mutated after construction; use `Colors = Colors with { Accent = ... }` to derive a modified copy
- All deprecated `Colors` properties (`MainAccent`, `DarkBackground`, `LightText`, etc.) have been removed
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
