using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPAJukebox.Data;
using MPAJukebox.Models;

namespace MPAJukebox.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var genres = await _context.Genres.Take(4).ToListAsync();
        
        var songs = await _context.Songs
            .Include(s => s.Genre)
            .Take(4)
            .ToListAsync();
        
        var viewModel = new HomeViewModel
        {
            Genres = genres,
            Songs = songs
        };
        
        return View(viewModel);
    }
}