using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MPAJukebox.Data;

namespace MPAJukebox.Controllers;

public class GenreController : Controller
{
    private readonly ApplicationDbContext _context;

    public GenreController(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> SongsByGenre(int id)
    {
        var songs = await _context.Songs
            .Include(s => s.Genre)
            .Where(s => s.GenreId == id)
            .ToListAsync();

        return View(songs);
    }
    
    public async Task<IActionResult> Index()
    {
        var genres = await _context.Genres.ToListAsync();
        return View(genres);
    }
}