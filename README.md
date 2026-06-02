[![.NET 10 Build](https://github.com/Lokyyn/Lucid/actions/workflows/dotnet_build.yml/badge.svg)](https://github.com/Lokyyn/Lucid/actions/workflows/dotnet_build.yml)
[![Publish package](https://github.com/Lokyyn/Lucid/actions/workflows/publish.yml/badge.svg?event=release)](https://github.com/Lokyyn/Lucid/actions/workflows/publish.yml)

[![](https://img.shields.io/nuget/dt/lucidui?color=004880&label=Downloads&logo=NuGet)](https://www.nuget.org/packages/LucidUI/)
[![](https://img.shields.io/nuget/v/lucidui?color=004880&label=Current%20Version&logo=NuGet)](https://www.nuget.org/packages/LucidUI/)

# Lucid

Lucid is a free WinForms control library providing dark-themed controls, a Visual Studio-style docking system, and a flexible theming engine. It builds upon [DarkUI](https://github.com/RobinPerris/DarkUI) and extends it with additional controls and features.

**Feel free to use this in your own projects — a link back to this repository is appreciated.**

> New features are added sporadically. If you find a bug, please open an issue.

---

## Installation

```
dotnet add package LucidUI
```

or via the NuGet Package Manager:

```
Install-Package LucidUI
```

---

## Controls

### Input

| Control | Description |
|---|---|
| `LucidButton` | Three styles available: `Normal`, `Flat`, and `Rounded` with configurable corner radius. |
| `LucidCheckBox` | |
| `LucidRadioButton` | |
| `LucidTextBox` | Themed border reacts to focus (`Accent`), hover (`BorderAccent`), and idle states. Supports `PlaceholderText` (native cue banner), `ShowClearButton` (inline × to clear input), and `Icon` (left-side image). |
| `LucidComboBox` | |
| `LucidNumericUpDown` | |
| `LucidDropdownList` | Fully custom-drawn dropdown backed by `LucidDropdownItem` objects, independent of the native ComboBox. |
| `LucidSlider` | Themed slider with `SingleValue` and `Range` mode (two handles). Supports configurable min/max/step, optional value label, and tick marks. |

### Display & Layout

| Control | Description |
|---|---|
| `LucidLabel` | |
| `LucidLinkLabel` | |
| `LucidTitle` | |
| `LucidSeparator` | |
| `LucidGroupBox` | |
| `LucidSectionPanel` | Panel that renders a labeled section header above its content area. |

### Complex Controls

| Control | Description |
|---|---|
| `LucidChipControl` | Scrollable chip/tag container with color coding, multi-select, and optional per-chip deletion. |
| `LucidTreeView` | Multi-select tree with drag-and-drop reordering, node badges, and an optional rounded selection style. |
| `LucidListView` | Custom-drawn list with multi-select and icon support. |
| `LucidProgressBar` | Standard and indeterminate mode; can optionally render the current percentage as text inside the bar. |
| `LucidFileDrop` | Drop zone that accepts dragged files with configurable allowed extensions. |
| `LucidPerformanceToolTip` | Tooltip that displays a numeric delta alongside a directional triangle (green ▲ / red ▼). |

### Strips & Menus

| Control |
|---|
| `LucidMenuStrip` |
| `LucidToolStrip` |
| `LucidStatusStrip` |
| `LucidContextMenu` |

---

## Docking

`LucidDockPanel` provides a Visual Studio-style docking system. Documents and tool windows can be arranged, split, and tabbed freely at runtime.

Key types:

| Type | Description |
|---|---|
| `LucidDockPanel` | The root container that manages all dock regions and groups. |
| `LucidDocument` | A tabbed document pane hosted inside the dock panel. |
| `LucidToolWindow` | A floating or docked side panel, equivalent to a VS tool window. |

---

## Theming

The `ThemeProvider` ships with built-in dark and light themes and supports registering fully custom themes at runtime. Base theme color properties can be overridden from outside the assembly.

---

## Getting Started

Step-by-step guides for the more complex controls are in [`sample/getting-started/`](sample/getting-started/):

- [ChipControl](sample/getting-started/chipcontrol.md)
- [FileDrop](sample/getting-started/filedrop.md)
- [TreeView](sample/getting-started/treeview.md)
