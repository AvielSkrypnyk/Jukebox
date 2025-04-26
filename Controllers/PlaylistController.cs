using Microsoft.AspNetCore.Mvc;

namespace MPAJukebox.Controllers;

public class PlaylistController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}