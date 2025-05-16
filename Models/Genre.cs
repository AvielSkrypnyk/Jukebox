using System.ComponentModel.DataAnnotations;

namespace MPAJukebox.Models;

public class Genre
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Song> Songs { get; set; }
}