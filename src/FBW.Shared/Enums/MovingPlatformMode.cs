namespace FBW.Shared.Enums;

public enum MovingPlatformMode
{
    /// <summary>
    /// Moves continuously between start and end point, pauses at each end.
    /// </summary>
    Auto,

    /// <summary>
    /// Stays at start point until activated by a linked activator,
    /// then moves to end point. Resets when deactivated.
    /// </summary>
    Triggered
}