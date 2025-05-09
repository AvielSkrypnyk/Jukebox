using Microsoft.EntityFrameworkCore;
using MPAJukebox.Models;

namespace MPAJukebox.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<DatabasePlaylist> Playlists { get; set; }
}