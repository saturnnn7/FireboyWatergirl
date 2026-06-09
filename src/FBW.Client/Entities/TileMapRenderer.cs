using FBW.Client.Components;
using Microsoft.Xna.Framework;
using Nez;

namespace FBW.Client.Entities;

/// <summary>
/// Entity that renders the TileMapComponent each frame.
/// </summary>
public class TileMapRenderer : RenderableComponent
{
    private readonly TileMapComponent _tileMap;

    public override float Width  => _tileMap.Width  * _tileMap.TileSize;
    public override float Height => _tileMap.Height * _tileMap.TileSize;

    public TileMapRenderer(TileMapComponent tileMap)
    {
        _tileMap    = tileMap;
        RenderLayer = 1; // LayerTiles
    }

    public override void Render(Batcher batcher, Camera camera)
    {
        _tileMap.Render(batcher);
    }
}