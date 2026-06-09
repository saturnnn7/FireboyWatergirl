using FBW.Shared.Enums;
using FBW.Shared.Models;
using Microsoft.Xna.Framework;
using Nez;

namespace FBW.Client.Components;

public class TileMapComponent : SceneComponent
{
    public int TileSize { get; private set; } = 32;
    public int Width    { get; private set; }
    public int Height   { get; private set; }

    private TileData[,] _tiles = new TileData[0, 0];

    public void LoadRoom(RoomData room)
    {
        Width  = room.Width;
        Height = room.Height;
        _tiles = new TileData[Width, Height];

        foreach (var tile in room.Tiles)
        {
            if (IsInBounds(tile.X, tile.Y))
                _tiles[tile.X, tile.Y] = tile;
        }
    }

    public void SetTile(int x, int y, TileData tile)
    {
        if (IsInBounds(x, y))
            _tiles[x, y] = tile;
    }

    public TileData GetTile(int x, int y)
        => IsInBounds(x, y) ? _tiles[x, y] : null;

    public bool OverlapsSolid(RectangleF bounds)
    {
        int minX = (int)MathF.Floor(bounds.Left   / TileSize);
        int minY = (int)MathF.Floor(bounds.Top    / TileSize);
        int maxX = (int)MathF.Floor(bounds.Right  / TileSize);
        int maxY = (int)MathF.Floor(bounds.Bottom / TileSize);

        for (int x = minX; x <= maxX; x++)
            for (int y = minY; y <= maxY; y++)
            {
                var tile = GetTile(x, y);
                if (tile != null && IsSolid(tile.Type))
                    return true;
            }

        return false;
    }

    public bool OverlapsHazard(RectangleF bounds, TileType hazardType)
    {
        int minX = (int)MathF.Floor(bounds.Left   / TileSize);
        int minY = (int)MathF.Floor(bounds.Top    / TileSize);
        int maxX = (int)MathF.Floor(bounds.Right  / TileSize);
        int maxY = (int)MathF.Floor(bounds.Bottom / TileSize);

        for (int x = minX; x <= maxX; x++)
            for (int y = minY; y <= maxY; y++)
            {
                var tile = GetTile(x, y);
                if (tile != null && tile.Type == hazardType)
                    return true;
            }

        return false;
    }

    public void Render(Batcher batcher)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var tile = _tiles[x, y];
                if (tile == null || tile.Type == TileType.Empty)
                    continue;

                var rect  = new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize);
                var color = GetDebugColor(tile.Type);
                batcher.DrawRect(rect, color);
            }
        }
    }

    private bool IsInBounds(int x, int y)
        => x >= 0 && y >= 0 && x < Width && y < Height;

    private static bool IsSolid(TileType type) => type switch
    {
        TileType.SolidBlock      => true,
        TileType.HalfBlock       => true,
        TileType.QuarterBlock    => true,
        TileType.SlopeBlock      => true,
        TileType.SmallSlopeBlock => true,
        TileType.StairBlockLeft  => true,
        TileType.StairBlockRight => true,
        TileType.FireTile        => true,
        TileType.WaterTile       => true,
        TileType.PoisonTile      => true,
        _                        => false
    };

    private static Color GetDebugColor(TileType type) => type switch
    {
        TileType.SolidBlock      => new Color(100, 100, 100),
        TileType.HalfBlock       => new Color(120, 120, 120),
        TileType.QuarterBlock    => new Color(140, 140, 140),
        TileType.SlopeBlock      => new Color(80,  80,  80 ),
        TileType.SmallSlopeBlock => new Color(90,  90,  90 ),
        TileType.StairBlockLeft  => new Color(110, 110, 110),
        TileType.StairBlockRight => new Color(110, 110, 110),
        TileType.Ladder          => new Color(180, 140, 60 ),
        TileType.FireTile        => new Color(220, 80,  20 ),
        TileType.WaterTile       => new Color(40,  120, 220),
        TileType.PoisonTile      => new Color(60,  180, 60 ),
        _                        => Color.Transparent
    };
}