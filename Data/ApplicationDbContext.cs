using Microsoft.EntityFrameworkCore;
using MPAJukebox.Models;

namespace MPAJukebox.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<SavedPlaylist> SavedPlaylists { get; set; }
}