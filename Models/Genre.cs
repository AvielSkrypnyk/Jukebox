using System.ComponentModel.DataAnnotations;

namespace MPAJukebox.Models;

public class Genre
{
    [Key]
    public int Id { get; set; }
    [MaxLength(255)]
    public required string Name { get; set; }
    public List<Song> Songs { get; set; } = [];
}