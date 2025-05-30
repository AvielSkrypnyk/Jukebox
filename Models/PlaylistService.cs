using Microsoft.EntityFrameworkCore;
using MPAJukebox.Data;
using MPAJukebox.Extensions;

namespace MPAJukebox.Models;

public class PlaylistService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ApplicationDbContext _context;
    private const string PlaylistKey = "Playlist";
    
    public PlaylistService(IHttpContextAccessor contextAccessor, ApplicationDbContext context)
    {
        _contextAccessor = contextAccessor;
        _context = context;
    }

    public SessionPlaylist GetPlaylist()
    {
        return _contextAccessor.HttpContext!.Session.GetObjectFromJson<SessionPlaylist>(PlaylistKey) ?? new SessionPlaylist();
    }
    
    public void AddSongToPlayList(Song song)
    {
        var playlist = GetPlaylist();
        playlist.Songs.Add(song);
        _contextAccessor.HttpContext!.Session.SetObjectAsJson(PlaylistKey, playlist);
    }
    
    public void RemoveSongFromPlayList(int id)
    {
        var playlist = GetPlaylist();
        playlist.Songs.RemoveAll(s => s.Id == id);
        _contextAccessor.HttpContext!.Session.SetObjectAsJson(PlaylistKey, playlist);
    }
    
    public void ClearPlaylist()
    {
        _contextAccessor.HttpContext!.Session.Remove(PlaylistKey);
    }

    public void RenamePlaylist(string newName)
    {
        var playlist = GetPlaylist();
        playlist.Name = newName;
        _contextAccessor.HttpContext!.Session.SetObjectAsJson(PlaylistKey, playlist);
    }
    
    public void SavePlaylistToDatabase(int userId)
    {
        var sessionPlaylist = GetPlaylist();
        if (sessionPlaylist.Songs.Any())
        {
            // Check if the user already has a playlist in the database
            var existingPlaylist = _context.Playlists.Include(x => x.Songs).FirstOrDefault(p => p.UserId == userId);
            if (existingPlaylist != null)
            {
                // Overwrite the existing playlist
                existingPlaylist.Name = sessionPlaylist.Name ?? "Untitled Playlist";
                existingPlaylist.Songs = sessionPlaylist.Songs
                    .Select(dto => _context.Songs.Find(dto.Id))
                    .Where(song => song != null)
                    .Select(song => song!).ToList(); // Ensure no nulls in the list, when in session playlist song is null and in database song is not null
                _context.Playlists.Update(existingPlaylist);
            }
            else
            {
                // Create a new database playlist
                var playlist = new DatabasePlaylist
                {
                    Name = sessionPlaylist.Name ?? "Untitled Playlist",
                    UserId = userId,
                    Songs = sessionPlaylist.Songs
                        .Select(dto => _context.Songs.Find(dto.Id))
                        .Where(song => song != null)
                        .Select(song => song!)
                        .ToList()
                };
                _context.Playlists.Add(playlist);
            }

            _context.SaveChanges();
        }
    }
}