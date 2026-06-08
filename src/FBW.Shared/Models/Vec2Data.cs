namespace FBW.Shared.Models;

/// <summary>
/// Serializable 2D vector used in level data.
/// Avoids a dependency on MonoGame or System.Numerics in FBW.Shared.
/// </summary>
public class Vec2Data
{
    public float X { get; set; }
    public float Y { get; set; }

    public Vec2Data() { }

    public Vec2Data(float x, float y)
    {
        X = x;
        Y = y;
    }
}