namespace MPAJukebox.Models;

public class HomeViewModel
{
    public required List<Genre> Genres { get; set; }
    public required List<Song> Songs { get; set; }
}