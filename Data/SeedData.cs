using MPAJukebox.Models;
using Microsoft.EntityFrameworkCore;

namespace MPAJukebox.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
        if (context.Genres.Any())
        {
            return;
        }
            
        // --- Seed Genres ---
        var genres = new[]
        {
            "Rock", "Pop", "Jazz", "Classical", "Hip-Hop", "Country", "Electronic", "Reggae"
        };
        context.Genres.AddRange(
            genres.Where(genreName => !context.Genres.Any(genre => genre.Name == genreName))
                .Select(genreName => new Genre { Name = genreName }));

        context.SaveChanges();
        

        // --- Seed Songs ---
        var songs = new[]
        {
            new { Title = "Bohemian Rhapsody", Artist = "Queen", Duration = 5.55, Genre = "Rock" },
            new { Title = "Stairway to Heaven", Artist = "Led Zeppelin", Duration = 8.02, Genre = "Rock" },
            new { Title = "Billie Jean", Artist = "Michael Jackson", Duration = 4.53, Genre = "Pop" },
            new { Title = "Shape of You", Artist = "Ed Sheeran", Duration = 3.53, Genre = "Pop" },
            new { Title = "So What", Artist = "Miles Davis", Duration = 9.22, Genre = "Jazz" },
            new { Title = "Take Five", Artist = "Dave Brubeck", Duration = 5.24, Genre = "Jazz" },
            new { Title = "Symphony No. 5", Artist = "Ludwig van Beethoven", Duration = 7.06, Genre = "Classical" },
            new { Title = "Clair de Lune", Artist = "Claude Debussy", Duration = 5.10, Genre = "Classical" },
            new { Title = "Lose Yourself", Artist = "Eminem", Duration = 5.26, Genre = "Hip-Hop" },
            new { Title = "Juicy", Artist = "The Notorious B.I.G.", Duration = 5.02, Genre = "Hip-Hop" },
            new { Title = "Jolene", Artist = "Dolly Parton", Duration = 2.42, Genre = "Country" },
            new { Title = "Take Me Home, Country Roads", Artist = "John Denver", Duration = 3.10, Genre = "Country" },
            new { Title = "Strobe", Artist = "Deadmau5", Duration = 10.37, Genre = "Electronic" },
            new { Title = "Animals", Artist = "Martin Garrix", Duration = 5.03, Genre = "Electronic" },
            new { Title = "No Woman, No Cry", Artist = "Bob Marley", Duration = 7.08, Genre = "Reggae" },
            new { Title = "Redemption Song", Artist = "Bob Marley", Duration = 3.47, Genre = "Reggae" }
        };

        foreach (var song in songs)
        {
            if (!context.Songs.Any(s => s.Title == song.Title && s.Artist == song.Artist))
            {
                context.Songs.Add(new Song
                {
                    Title = song.Title,
                    Artist = song.Artist,
                    Duration = song.Duration,
                    Genre = context.Genres.First(g => g.Name == song.Genre)
                });
            }
        }

        context.SaveChanges();
    }
}