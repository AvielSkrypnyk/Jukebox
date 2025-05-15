namespace MPAJukebox.Models;

public class SongDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public double Duration { get; set; }
    public string Genre { get; set; }
}