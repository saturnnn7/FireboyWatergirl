using Microsoft.Xna.Framework;
using Nez;

namespace FBW.Client.Components;

/// <summary>
/// Handles gravity, velocity integration, and AABB tile collision resolution.
/// Reads movement intent from InputComponent and moves the entity accordingly.
/// Collision is resolved separately on X and Y axes to avoid corner-sticking.
/// </summary>
public class PhysicsComponent : Component, IUpdatable
{
    // ── Public state ───────────────────────────────────────────────────────────

    public Vector2 Velocity   { get; private set; }
    public bool    IsGrounded { get; private set; }
    public bool    IsMoving   => MathF.Abs(Velocity.X) > 0.1f;

    // ── Settings ───────────────────────────────────────────────────────────────

    public float MoveSpeed    = 220f;
    public float JumpForce    = 580f;
    public float Gravity      = 1800f;
    public float MaxFallSpeed = 1200f;

    /// <summary>Size of the player's AABB collider in pixels.</summary>
    public Vector2 ColliderSize = new(26, 30);

    // ── Dependencies ───────────────────────────────────────────────────────────

    private InputComponent _input = null!;

    // ── Internal state ─────────────────────────────────────────────────────────

    private Vector2 _velocity;
    private bool    _isGrounded;

    // ── Lifecycle ──────────────────────────────────────────────────────────────

    public override void OnAddedToEntity()
    {
        _input = Entity.GetComponent<InputComponent>();
    }

    public void Update()
    {
        float dt = Time.DeltaTime;

        ApplyInput(dt);
        ApplyGravity(dt);
        MoveAndCollide(dt);

        Velocity   = _velocity;
        IsGrounded = _isGrounded;
    }

    // ── Physics steps ──────────────────────────────────────────────────────────

    private void ApplyInput(float dt)
    {
        _velocity.X = _input.MoveDirection * MoveSpeed;

        if (_isGrounded && _input.JumpPressed)
        {
            _velocity.Y = -JumpForce;
            _isGrounded = false;
        }
    }

    private void ApplyGravity(float dt)
    {
        if (!_isGrounded)
        {
            _velocity.Y += Gravity * dt;
            _velocity.Y  = MathF.Min(_velocity.Y, MaxFallSpeed);
        }
    }

    private void MoveAndCollide(float dt)
    {
        var tileMap = Entity.Scene.GetSceneComponent<TileMapComponent>();
        if (tileMap == null) return;
    
        Vector2 delta = _velocity * dt;
    
        // ── Resolve X axis ────────────────────────────────────────────────────
        if (delta.X != 0)
        {
            Entity.Position += new Vector2(delta.X, 0);
    
            if (tileMap.OverlapsSolid(GetBounds()))
            {
                Entity.Position -= new Vector2(delta.X, 0);
                _velocity.X      = 0;
            }
        }
    
        // ── Resolve Y axis — step by step to prevent tunneling ────────────────
        if (delta.Y != 0)
        {
            float remaining = delta.Y;
            float stepSize  = 4f;
            bool  hitY      = false;
    
            while (MathF.Abs(remaining) > 0.001f)
            {
                float step = MathF.Sign(remaining) * MathF.Min(MathF.Abs(remaining), stepSize);
                Entity.Position += new Vector2(0, step);
                remaining       -= step;
    
                if (tileMap.OverlapsSolid(GetBounds()))
                {
                    Entity.Position -= new Vector2(0, step);
                    hitY             = true;
                    break;
                }
            }
    
            if (hitY)
            {
                if (delta.Y > 0)
                    _isGrounded = true;
    
                _velocity.Y = 0;
            }
            else
            {
                _isGrounded = false;
            }
        }
    
        // ── Ground check — runs every frame regardless of Y movement ──────────
        // Checks if there is a solid tile 2px below the feet.
        // This ensures _isGrounded resets when the ground disappears under the player
        // (e.g. walking off a platform edge, or a moving platform moving away).
        var groundCheckBounds = new RectangleF(
            GetBounds().X,
            GetBounds().Y + GetBounds().Height,
            GetBounds().Width,
            2f
        );
    
        bool groundBelow = tileMap.OverlapsSolid(groundCheckBounds);
    
        if (!groundBelow)
            _isGrounded = false;
    }

    // ── Helpers ────────────────────────────────────────────────────────────────

    public RectangleF GetBounds()
    {
        return new RectangleF(
            Entity.Position.X - ColliderSize.X / 2f,
            Entity.Position.Y - ColliderSize.Y / 2f,
            ColliderSize.X,
            ColliderSize.Y
        );
    }
}