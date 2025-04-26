using Microsoft.AspNetCore.Mvc;

namespace MPAJukebox.Controllers;

public class SongController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}