using FBW.Shared.Enums;
using System.Collections.Generic;

namespace FBW.Client.Settings;

/// <summary>
/// Root model for settings.json saved locally on disk.
/// </summary>
public class SettingsData
{
    public InputGroupData GroupA { get; set; } = InputGroupData.DefaultGroupA();
    public InputGroupData GroupB { get; set; } = InputGroupData.DefaultGroupB();

    /// <summary>
    /// Maps each player to an input group id ("A" or "B").
    /// Can be swapped at any time.
    /// </summary>
    public Dictionary<PlayerType, string> GroupAssignments { get; set; } = new()
    {
        { PlayerType.Fireboy,   "A" },
        { PlayerType.Watergirl, "B" }
    };
}

/// <summary>
/// One input group — a named set of key bindings for one player slot.
/// </summary>
public class InputGroupData
{
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Maps action names to key names.
    /// Key names match Microsoft.Xna.Framework.Input.Keys enum names (e.g. "A", "Left", "Space").
    /// </summary>
    public Dictionary<string, string> Bindings { get; set; } = [];

    public static InputGroupData DefaultGroupA() => new()
    {
        Id = "A",
        Bindings = new Dictionary<string, string>
        {
            { "moveLeft",  "A"     },
            { "moveRight", "D"     },
            { "jump",      "W"     }
        }
    };

    public static InputGroupData DefaultGroupB() => new()
    {
        Id = "B",
        Bindings = new Dictionary<string, string>
        {
            { "moveLeft",  "Left"  },
            { "moveRight", "Right" },
            { "jump",      "Up"    }
        }
    };
}