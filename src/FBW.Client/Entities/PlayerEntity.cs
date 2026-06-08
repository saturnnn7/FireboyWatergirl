using Nez;
using FBW.Shared.Enums;

namespace FBW.Client.Entities;

/// <summary>
/// Represents a player entity (Fireboy or Watergirl).
/// </summary>
public class PlayerEntity : Entity
{
    public PlayerType Type { get; private set; }

    public PlayerEntity(PlayerType type)
    {
        Type = type;
    }

    // TODO: add components (physics, input, animator, collider)
}