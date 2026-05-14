# Changelog

All notable changes to this project will be documented in this file.

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
