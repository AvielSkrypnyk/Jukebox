using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPAJukebox.Data;
using MPAJukebox.Extensions;
using MPAJukebox.Models;

namespace MPAJukebox.Controllers;

public class SongController : Controller
{
    private readonly ApplicationDbContext _context;

    public SongController(ApplicationDbContext context)
    {
        _context = context;
    }

    
    [HttpGet]
    [Route("Song/IndexByGenre/{genreId}")]
    public async Task<IActionResult> Index(int genreId)
    {
        var songs = await _context.Songs.Include(s => s.Genre)
            .Where(s => s.GenreId == genreId)
            .ToListAsync();
        return View(songs);
    }

    public async Task<IActionResult> Details(int id)
    {
        var song = await _context.Songs.Include(s => s.Genre)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (song == null)
        {
            return NotFound();
        }
        return View(song);
    }

    public IActionResult AddToPlaylist(int id)
    {
        var playlist = HttpContext.Session.GetObjectFromJson<SessionPlaylist>("Playlist") ?? new SessionPlaylist();

        var song = _context.Songs.Find(id);
        if (song != null)
        {
            playlist.Songs.Add(song);
            HttpContext.Session.SetObjectAsJson("Playlist", playlist);
        }
        return RedirectToAction("Index", "Playlist");
    }

    [HttpGet]
    [Route("Song/Index")]
    public async Task<IActionResult> Index()
    {
        var songs = await _context.Songs.Include(s => s.Genre).ToListAsync();
        return View(songs);
    }
}