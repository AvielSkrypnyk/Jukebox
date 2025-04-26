using Microsoft.AspNetCore.Mvc;

namespace MPAJukebox.Controllers;

public class AccountController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}