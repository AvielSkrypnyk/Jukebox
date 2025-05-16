using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPAJukebox.Data;
using MPAJukebox.Extensions;
using MPAJukebox.Models;

namespace MPAJukebox.Controllers;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login");
        }
        return View(user);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Password == password && u.Email == email);
        if (user != null)
        {
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);

            // Load the user's playlist into the session
            var playlist = await _context.Playlists.Include(p => p.Songs)
                .ThenInclude(s => s.Genre)
                .FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (playlist != null)
            {
                var sessionPlaylist = new SessionPlaylist
                {
                    Name = playlist.Name,
                    Songs = playlist.Songs.ToList()
                };
                HttpContext.Session.SetObjectAsJson("Playlist", sessionPlaylist);
            }
            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError("", "Invalid email or password");
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}