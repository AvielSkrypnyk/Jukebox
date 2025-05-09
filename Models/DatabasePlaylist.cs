using System.ComponentModel.DataAnnotations.Schema;

namespace MPAJukebox.Models;
[Table("Playlists")]
public class DatabasePlaylist
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public int UserId { get; set; }
    public List<Song> Songs { get; set; } = [];
}