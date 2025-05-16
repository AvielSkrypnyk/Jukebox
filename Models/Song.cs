using System.ComponentModel.DataAnnotations;

namespace MPAJukebox.Models;

public class Song
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public double Duration { get; set; }
    public int GenreId { get; set; }
    public Genre Genre { get; set; }
    public List<DatabasePlaylist> Playlists { get; set; } = [];
}