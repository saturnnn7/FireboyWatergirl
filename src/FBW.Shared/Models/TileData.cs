using FBW.Shared.Enums;

namespace FBW.Shared.Models;

public class TileData
{
    /// <summary>Grid column (tile units).</summary>
    public int X { get; set; }

    /// <summary>Grid row (tile units).</summary>
    public int Y { get; set; }

    public TileType Type { get; set; }

    public TileRotation Rotation { get; set; } = TileRotation.Deg0;

    /// <summary>
    /// Visual theme template name (e.g. "default", "cave", "ice").
    /// Does not affect collision.
    /// </summary>
    public string TextureTemplate { get; set; } = "default";
}