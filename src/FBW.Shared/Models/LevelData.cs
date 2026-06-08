namespace FBW.Shared.Models;

public class LevelData
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public Guid AuthorId { get; set; }

    /// <summary>UTC timestamp of creation.</summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>UTC timestamp of last edit.</summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>Whether the level is published and visible to other players.</summary>
    public bool IsPublished { get; set; } = false;

    public List<RoomData> Rooms { get; set; } = [];
}