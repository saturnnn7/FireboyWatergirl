namespace FBW.Shared.Enums;

public enum EntityType
{
    // Players
    SpawnPoint,

    // Goal
    Door,

    // Collectibles
    GemFire,
    GemWater,
    GemNeutral,

    // Puddles
    PuddleFire,
    PuddleWater,
    PuddlePoison,

    // Activators
    Lever,
    ButtonInstant,
    ButtonTimer,

    // Activated objects
    Piston,
    MovingPlatform,

    // Physics objects
    Box,

    // Laser system
    LightSource,
    LightSlider,
    Mirror,
    LightReceiver
}