using FBW.Client.Components;
using FBW.Client.Entities;
using FBW.Client.Systems;
using FBW.Shared.Enums;
using FBW.Shared.Models;
using Microsoft.Xna.Framework;
using Nez;

namespace FBW.Client.Scenes;

public class GameScene : Scene
{
    public override void Initialize()
    {
        base.Initialize();

        ClearColor = new Color(30, 30, 40);

        // ── TileMap ───────────────────────────────────────────────────────────
        var room    = BuildTestRoom();
        var tileMap = new TileMapComponent();
        tileMap.LoadRoom(room);
        AddSceneComponent(tileMap);

        // ── TileMap renderer entity ───────────────────────────────────────────
        var tileMapEntity = CreateEntity("tilemap");
        tileMapEntity.AddComponent(new TileMapRenderer(tileMap));

        // ── Spawn Fireboy ─────────────────────────────────────────────────────
        var fireboy = CreateEntity("fireboy");
        fireboy.Position = new Vector2(3 * 32, 10 * 32);
        fireboy.AddComponent(new InputComponent(PlayerType.Fireboy));
        fireboy.AddComponent(new PhysicsComponent());
        var fbSprite = fireboy.AddComponent(new PrototypeSpriteRenderer(26, 30));
        fbSprite.Color       = new Color(220, 80, 20);
        fbSprite.LocalOffset = new Vector2(0, -15);


        // ── Spawn Watergirl ───────────────────────────────────────────────────
        var watergirl = CreateEntity("watergirl");
        watergirl.Position = new Vector2(5 * 32, 10 * 32);
        watergirl.AddComponent(new InputComponent(PlayerType.Watergirl));
        watergirl.AddComponent(new PhysicsComponent());
        var wgSprite = watergirl.AddComponent(new PrototypeSpriteRenderer(26, 30));
        wgSprite.Color       = new Color(40, 120, 220);
        wgSprite.LocalOffset = new Vector2(0, -15);


        // ── Camera ────────────────────────────────────────────────────────────
        var cam = AddSceneComponent(new CameraSystem());
        cam.SetTargets(fireboy, watergirl);
    }

    private static RoomData BuildTestRoom()
    {
        var room = new RoomData { Width = 30, Height = 18 };

        // Floor
        for (int x = 0; x < 30; x++)
            room.Tiles.Add(new TileData { X = x, Y = 17, Type = TileType.SolidBlock });

        // Walls
        for (int y = 0; y < 18; y++)
        {
            room.Tiles.Add(new TileData { X = 0,  Y = y, Type = TileType.SolidBlock });
            room.Tiles.Add(new TileData { X = 29, Y = y, Type = TileType.SolidBlock });
        }

        // Platforms
        for (int x = 5; x <= 10; x++)
            room.Tiles.Add(new TileData { X = x, Y = 13, Type = TileType.SolidBlock });

        for (int x = 14; x <= 20; x++)
            room.Tiles.Add(new TileData { X = x, Y = 10, Type = TileType.SolidBlock });

        // Hazard tiles
        for (int x = 8; x <= 12; x++)
            room.Tiles.Add(new TileData { X = x, Y = 16, Type = TileType.FireTile });

        return room;
    }
}