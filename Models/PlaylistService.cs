using MPAJukebox.Data;
using MPAJukebox.Extensions;

namespace MPAJukebox.Models;

public class PlaylistService
{
    private readonly ISession _session;
    private readonly ApplicationDbContext _context;
    private const string PlaylistKey = "Playlist";
    
    public PlaylistService(ISession session, ApplicationDbContext context)
    {
        _session = session;
        _context = context;
    }

    public SessionPlaylist GetPlaylist()
    {
        return _session.GetObjectFromJson<SessionPlaylist>(PlaylistKey) ?? new SessionPlaylist();
    }
    
    public void AddSongToPlayList(Song song)
    {
        var playlist = GetPlaylist();
        var songDto = new SongDto()
        {
            Id = song.Id,
            Title = song.Title,
            Artist = song.Artist,
            Duration = song.Duration,
            Genre = song.Genre.Name
        };
        playlist.Songs.Add(songDto);
        _session.SetObjectAsJson(PlaylistKey, playlist);
    }
    
    public void RemoveSongFromPlayList(int id)
    {
        var playlist = GetPlaylist();
        playlist.Songs.RemoveAll(s => s.Id == id);
        _session.SetObjectAsJson(PlaylistKey, playlist);
    }
    
    public void ClearPlaylist()
    {
        _session.Remove(PlaylistKey);
    }

    public void RenamePlaylist(string newName)
    {
        var playlist = GetPlaylist();
        playlist.Name = newName;
        _session.SetObjectAsJson(PlaylistKey, playlist);
    }
    
    public void SavePlaylistToDatabase(int userId)
    {
        var sessionPlaylist = GetPlaylist();
        if (sessionPlaylist.Songs.Any())
        {
            // Check if the user already has a playlist in the database
            var existingPlaylist = _context.Playlists.FirstOrDefault(p => p.UserId == userId);
            if (existingPlaylist != null)
            {
                // Overwrite the existing playlist
                existingPlaylist.Name = sessionPlaylist.Name ?? "Untitled Playlist";
                existingPlaylist.Songs = sessionPlaylist.Songs
                    .Select(dto => _context.Songs.Find(dto.Id))
                    .Where(song => song != null)
                    .ToList();
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
                        .ToList()
                };
                _context.Playlists.Add(playlist);
            }

            _context.SaveChanges();
        }
    }
}