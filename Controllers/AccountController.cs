using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPAJukebox.Data;
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
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        if (user != null)
        {
            HttpContext.Session.SetInt32("UserId", user.Id);
            return RedirectToAction("Index", "Genre");
        }
        ModelState.AddModelError("", "Invalid username or password");
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}