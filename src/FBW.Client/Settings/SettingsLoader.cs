using System;
using System.IO;
using System.Text.Json;

namespace FBW.Client.Settings;

/// <summary>
/// Reads and writes settings.json from the game's executable directory.
/// Creates a file with defaults if it doesn't exist yet.
/// </summary>
public static class SettingsLoader
{
    private static readonly string SettingsPath = Path.Combine(
        AppContext.BaseDirectory,
        "settings.json"
    );

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
    };

    /// <summary>
    /// Loads settings from disk. If the file doesn't exist, returns defaults and saves them.
    /// </summary>
    public static SettingsData Load()
    {
        if (!File.Exists(SettingsPath))
        {
            var defaults = new SettingsData();
            Save(defaults);
            return defaults;
        }

        try
        {
            var json = File.ReadAllText(SettingsPath);
            return JsonSerializer.Deserialize<SettingsData>(json, JsonOptions)
                   ?? new SettingsData();
        }
        catch
        {
            // If the file is corrupted, fall back to defaults
            var defaults = new SettingsData();
            Save(defaults);
            return defaults;
        }
    }

    /// <summary>
    /// Saves current settings to disk immediately.
    /// </summary>
    public static void Save(SettingsData settings)
    {
        var json = JsonSerializer.Serialize(settings, JsonOptions);
        File.WriteAllText(SettingsPath, json);
    }
}