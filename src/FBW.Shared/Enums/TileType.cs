namespace FBW.Shared.Enums;

public enum TileType
{
    Empty,

    // Basic blocks
    SolidBlock,
    HalfBlock,
    QuarterBlock,
    SlopeBlock,
    SmallSlopeBlock,
    StairBlockLeft,
    StairBlockRight,
    Ladder,

    // Hazard blocks
    FireTile,
    WaterTile,
    PoisonTile
}