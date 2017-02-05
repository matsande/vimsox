# VimSoX
Provides Vim-like navigation to the Visual Studio Solution Explorer.

# Installation 
Simplest way to install is through the Extension Manager or the Visual Studio gallery URL HERE.
Or if building from source, use the resulting VimSox.vsix.

Currently supported Visual Studio versions are 2015+.

# Supported Commands
| Key | Command         | Description                                                                                     |
|-----|-----------------|-------------------------------------------------------------------------------------------------|
| h   | MoveLeft        | Moves the selection one step to the left, collapses expanded items when applicable.             |
| j   | MoveDown        | Moves the selection one step down.                                                              |
| k   | MoveUp          | Moves the selection one step up.                                                                |
| l   | MoveRight       | Moves the selection one step to the right, expands collapsed items when applicable.             |
| G   | MoveBottom      | Moves the selection to the bottom of the Solution Explorer.                                     |
| gg  | MoveTop         | Moves the selection to the top of the Solution Explorer.                                        |
| i   | EnterInsertMode | Stops all processing of keys with the exception of esc, i.e default Solution Explorer behavior. |
| Esc | ExitInsertMode  | Exits InsertMode, resumes handling of navigation keys.                                          |

# Reporting Problems
Please use the [issue tracker](https://github.com/matsande/vimsox/issues).

