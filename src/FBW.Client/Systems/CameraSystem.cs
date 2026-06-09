using Microsoft.Xna.Framework;
using Nez;

namespace FBW.Client.Systems;

/// <summary>
/// Follows the midpoint between both players with smooth lerp.
/// </summary>
public class CameraSystem : SceneComponent
{
    private Entity _targetA;
    private Entity _targetB;

    private const float LerpSpeed = 5f;
    private const float ZoomLevel = 1f;

    public void SetTargets(Entity a, Entity b)
    {
        _targetA = a;
        _targetB = b;
    }

    public override void Update()
    {
        if (_targetA == null) return;

        Vector2 target = _targetA.Position;

        if (_targetB != null)
            target = (_targetA.Position + _targetB.Position) / 2f;

        var cam      = Scene.Camera;
        cam.Position = Vector2.Lerp(cam.Position, target, LerpSpeed * Time.DeltaTime);
        cam.Zoom     = ZoomLevel;
    }
}