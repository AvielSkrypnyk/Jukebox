using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPAJukebox.Models;
[Table("Playlists")]
public class DatabasePlaylist
{
    [Key]
    public int Id { get; set; }
    [MaxLength(255)]
    public required string Name { get; set; }
    
    public int UserId { get; set; }
    public List<Song> Songs { get; set; } = [];
}