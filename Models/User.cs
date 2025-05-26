using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MPAJukebox.Models;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    [Key]
    public int Id { get; set; }
    [MaxLength(255)] // Just a random length, can be adjusted
    public required string Username { get; set; }
    [MaxLength(255)]
    public required string Email { get; set; }
    [MaxLength(255)]
    public required string Password { get; set; }
}  