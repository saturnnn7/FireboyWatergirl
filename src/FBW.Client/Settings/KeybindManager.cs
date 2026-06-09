using System;
using FBW.Shared.Enums;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FBW.Client.Settings;

/// <summary>
/// Central manager for input groups and player assignments.
/// Loaded once at startup, persisted when changed.
/// </summary>
public static class KeybindManager
{
    private static SettingsData _settings = new();

    /// <summary>
    /// Loads settings from disk and initializes the manager.
    /// Call once in Game1.Initialize().
    /// </summary>
    public static void Initialize()
    {
        _settings = SettingsLoader.Load();
    }

    /// <summary>
    /// Returns the Keys value bound to an action for a given player.
    /// Returns Keys.None if the binding is missing or invalid.
    /// </summary>
    public static Keys GetKey(PlayerType player, string action)
    {
        var groupId   = _settings.GroupAssignments[player];
        var groupData = groupId == "A" ? _settings.GroupA : _settings.GroupB;

        if (!groupData.Bindings.TryGetValue(action, out var keyName))
            return Keys.None;

        return Enum.TryParse<Keys>(keyName, out var key) ? key : Keys.None;
    }

    /// <summary>
    /// Swaps input group assignments between Fireboy and Watergirl.
    /// Persists the change to disk immediately.
    /// </summary>
    public static void SwapGroups()
    {
        var fireboyGroup   = _settings.GroupAssignments[PlayerType.Fireboy];
        var watergirlGroup = _settings.GroupAssignments[PlayerType.Watergirl];

        _settings.GroupAssignments[PlayerType.Fireboy]   = watergirlGroup;
        _settings.GroupAssignments[PlayerType.Watergirl] = fireboyGroup;

        SettingsLoader.Save(_settings);
    }

    /// <summary>
    /// Rebinds an action for a given player to a new key.
    /// Persists the change to disk immediately.
    /// </summary>
    public static void Rebind(PlayerType player, string action, Keys newKey)
    {
        var groupId   = _settings.GroupAssignments[player];
        var groupData = groupId == "A" ? _settings.GroupA : _settings.GroupB;

        groupData.Bindings[action] = newKey.ToString();
        SettingsLoader.Save(_settings);
    }

    /// <summary>
    /// Returns a read-only view of all bindings for a given player.
    /// Used by the settings UI to display current keybinds.
    /// </summary>
    public static IReadOnlyDictionary<string, string> GetBindings(PlayerType player)
    {
        var groupId   = _settings.GroupAssignments[player];
        var groupData = groupId == "A" ? _settings.GroupA : _settings.GroupB;
        return groupData.Bindings;
    }
}