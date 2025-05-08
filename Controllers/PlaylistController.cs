using Microsoft.AspNetCore.Mvc;
using MPAJukebox.Data;
using MPAJukebox.Extensions;
using MPAJukebox.Models;

namespace MPAJukebox.Controllers;

public class PlaylistController : Controller
{
    private readonly PlaylistService _playlistService;
    
    public PlaylistController(IHttpContextAccessor httpContextAccessor)
    {
        _playlistService = new PlaylistService(httpContextAccessor.HttpContext.Session);
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
}