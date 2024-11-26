using PlayStreamAPI.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public ICollection<Playlist> Playlists { get; set; }   
}
