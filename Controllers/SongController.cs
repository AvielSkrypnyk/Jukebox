using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPAJukebox.Data;
using MPAJukebox.Extensions;
using MPAJukebox.Models;

namespace MPAJukebox.Controllers;

public class SongController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly PlaylistService _playlistService;

    public SongController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _playlistService = new PlaylistService(httpContextAccessor.HttpContext.Session, context);
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
        var song = _context.Songs.Include(s => s.Genre).FirstOrDefault(s => s.Id == id);
        if (song != null)
        {
            _playlistService.AddSongToPlayList(song);
        }
        return RedirectToAction("Index", "Playlist");
    }
    
    public IActionResult RemoveFromPlaylist(int id)
    {
        _playlistService.RemoveSongFromPlayList(id);
        return RedirectToAction("Index", "Playlist");
    }
    
    public IActionResult ClearPlaylist()
    {
        _playlistService.ClearPlaylist();
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