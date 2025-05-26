using System.ComponentModel.DataAnnotations;

namespace MPAJukebox.Models;

public class Song
{
    [Key]
    public int Id { get; set; }
    [MaxLength(255)]
    public required string Title { get; set; }
    [MaxLength(255)]
    public required string Artist { get; set; }
    public double Duration { get; set; }
    [MaxLength(255)]
    public required Genre Genre { get; set; }
    public List<DatabasePlaylist> Playlists { get; set; } = [];
}