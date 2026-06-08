using FBW.Shared.Enums;

namespace FBW.Shared.Models;

public class EntityData
{
    /// <summary>Unique identifier within the room. Used by the linking system.</summary>
    public string Id { get; set; } = string.Empty;

    public EntityType Type { get; set; }

    /// <summary>World position in pixels.</summary>
    public Vec2Data Position { get; set; } = new();

    // ── Linking ────────────────────────────────────────────────────────────────

    /// <summary>
    /// IDs of entities this activator controls.
    /// Used by: Lever, ButtonInstant, ButtonTimer, LightReceiver.
    /// </summary>
    public List<string> LinkedTo { get; set; } = [];

    // ── ButtonTimer ────────────────────────────────────────────────────────────

    /// <summary>
    /// How long linked entities stay active after ButtonTimer is triggered (in seconds).
    /// Timer resets if triggered again before it expires.
    /// Used by ButtonTimer.
    /// </summary>
    public float? Duration { get; set; }

    // ── MovingPlatform ─────────────────────────────────────────────────────────

    public MovingPlatformMode? Mode { get; set; }

    /// <summary>End point of the platform path in pixels.</summary>
    public Vec2Data? PathEnd { get; set; }

    /// <summary>Pause at each end of the path in seconds. Used in Auto mode.</summary>
    public float? PauseDuration { get; set; }

    // ── Door ───────────────────────────────────────────────────────────────────

    public DoorExitType? ExitType { get; set; }

    /// <summary>Target room index. Used when ExitType is NextRoom.</summary>
    public int? TargetRoomId { get; set; }

    /// <summary>Which player this door belongs to.</summary>
    public PlayerType? AssignedPlayer { get; set; }

    // ── SpawnPoint ─────────────────────────────────────────────────────────────

    /// <summary>Which player spawns here.</summary>
    public PlayerType? SpawnPlayer { get; set; }

    // ── LightSource ────────────────────────────────────────────────────────────

    /// <summary>Initial angle in degrees (0–90). Used by LightSource.</summary>
    public float? LightAngle { get; set; }

    /// <summary>ID of the LightSlider that controls this source.</summary>
    public string? LightSliderId { get; set; }

    // ── Puddle ─────────────────────────────────────────────────────────────────

    /// <summary>Width in pixels. Used by puddle entities.</summary>
    public float? Width { get; set; }

    /// <summary>Height in pixels. Used by puddle entities.</summary>
    public float? Height { get; set; }
}