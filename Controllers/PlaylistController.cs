using Microsoft.AspNetCore.Mvc;
using MPAJukebox.Data;
using MPAJukebox.Extensions;
using MPAJukebox.Models;

namespace MPAJukebox.Controllers;

public class PlaylistController : Controller
{
    private readonly PlaylistService _playlistService;
    private readonly ApplicationDbContext _context;
    
    public PlaylistController(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
    {
        _context = context;
        _playlistService = new PlaylistService(httpContextAccessor.HttpContext.Session, context);
    }
    
    public IActionResult Index()
    {
        var playlist = _playlistService.GetPlaylist();
        return View(playlist);
    }

    public IActionResult Clear()
    {
        _playlistService.ClearPlaylist();
        return RedirectToAction("Index");
    }
    
    public IActionResult RenamePlaylist(string newName)
    {
        _playlistService.RenamePlaylist(newName);
        return RedirectToAction("Index");
    }
    
    public IActionResult SavePlaylist()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId.HasValue)
        {
            _playlistService.SavePlaylistToDatabase(userId.Value);
        }
        return RedirectToAction("Index");
    }
}