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

    public async Task<IActionResult> Index()
    {
        var genres = await _context.Genres.ToListAsync();
        return View(genres);
    }
}