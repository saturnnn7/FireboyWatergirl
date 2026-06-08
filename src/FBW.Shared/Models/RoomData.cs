using FBW.Shared.Enums;

namespace FBW.Shared.Models;

public class RoomData
{
    /// <summary>Room index within the level (1-based).</summary>
    public int Id { get; set; }

    /// <summary>Room width in tile units.</summary>
    public int Width { get; set; }

    /// <summary>Room height in tile units.</summary>
    public int Height { get; set; }

    /// <summary>
    /// Which player(s) are present in this room.
    /// Used for single-player mode where rooms are assigned to one character.
    /// </summary>
    public PlayerType AssignedPlayer { get; set; } = PlayerType.Both;

    /// <summary>Visual theme for all tiles in this room.</summary>
    public string TileTheme { get; set; } = "default";

    public List<TileData> Tiles { get; set; } = [];

    public List<EntityData> Entities { get; set; } = [];
}