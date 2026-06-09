namespace FBW.Client.Core;

public static class Constants
{
    // Window
    public const int    ScreenWidth  = 1280;
    public const int    ScreenHeight = 720;
    public const string WindowTitle  = "Fireboy & Watergirl";

    // Physics
    public const float Gravity      = 1800f;
    public const float MaxFallSpeed = 1200f;

    // Tiles
    public const int TileSize = 32;

    // Render layers
    public const int LayerBackground = 0;
    public const int LayerTiles      = 1;
    public const int LayerEntities   = 2;
    public const int LayerPlayers    = 3;
    public const int LayerLasers     = 4;
    public const int LayerUI         = 5;
}