namespace MPAJukebox.Models;

public class SessionPlaylist
{
    public string? Name { get; set; }
    public List<Song> Songs { get; set; } = [];
}