using MPAJukebox.Extensions;

namespace MPAJukebox.Models;

public class PlaylistService
{
    private readonly ISession _session;
    private const string PlaylistKey = "Playlist";
    
    public PlaylistService(ISession session)
    {
        _session = session;
    }

    public SessionPlaylist GetPlaylist()
    {
        return _session.GetObjectFromJson<SessionPlaylist>(PlaylistKey) ?? new SessionPlaylist();
    }
    
    public void AddSongToPlayList(Song song)
    {
        var playlist = GetPlaylist();
        playlist.Songs.Add(song);
        _session.SetObjectAsJson(PlaylistKey, playlist);
    }
    
    public void RemoveSongFromPlayList(Song song)
    {
        var playlist = GetPlaylist();
        playlist.Songs.RemoveAll(s => s.Id == song.Id);
        _session.SetObjectAsJson(PlaylistKey, playlist);
    }
    
    public void ClearPlaylist()
    {
        _session.Remove(PlaylistKey);
    }
}