using FBW.Client.Settings;
using FBW.Shared.Enums;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace FBW.Client.Components;

/// <summary>
/// Reads keyboard input for a specific player using their assigned keybind group.
/// Exposes simple intent flags consumed by PhysicsComponent and other systems.
/// Does not contain any physics or game logic — only input state.
/// </summary>
public class InputComponent : Component, IUpdatable
{
    // ── Public intent flags (read by other components) ─────────────────────────

    /// <summary>-1 = left, 0 = none, 1 = right</summary>
    public int MoveDirection { get; private set; }

    public bool JumpPressed  { get; private set; }
    public bool JumpHeld     { get; private set; }

    // ── Private state ──────────────────────────────────────────────────────────

    private readonly PlayerType _playerType;

    private Keys _keyLeft;
    private Keys _keyRight;
    private Keys _keyJump;

    private bool _prevJumpHeld;

    // ── Constructor ────────────────────────────────────────────────────────────

    public InputComponent(PlayerType playerType)
    {
        _playerType = playerType;
    }

    // ── Lifecycle ──────────────────────────────────────────────────────────────

    public override void OnAddedToEntity()
    {
        RefreshBindings();
    }

    public void Update()
    {
        // Refresh bindings every frame so remapping is instant
        RefreshBindings();

        var keyboard = Keyboard.GetState();

        bool left  = keyboard.IsKeyDown(_keyLeft);
        bool right = keyboard.IsKeyDown(_keyRight);
        bool jump  = keyboard.IsKeyDown(_keyJump);

        MoveDirection = right ? 1 : left ? -1 : 0;

        // JumpPressed is true only on the first frame the key is held
        JumpPressed  = jump && !_prevJumpHeld;
        JumpHeld     = jump;

        _prevJumpHeld = jump;
    }

    // ── Helpers ────────────────────────────────────────────────────────────────

    /// <summary>
    /// Re-reads key bindings from KeybindManager.
    /// Called every frame so live remapping works without restart.
    /// </summary>
    private void RefreshBindings()
    {
        _keyLeft  = KeybindManager.GetKey(_playerType, "moveLeft");
        _keyRight = KeybindManager.GetKey(_playerType, "moveRight");
        _keyJump  = KeybindManager.GetKey(_playerType, "jump");
    }
}