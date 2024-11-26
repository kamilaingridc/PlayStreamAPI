namespace PlayStreamAPI.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
        public ICollection<ItemPlaylist> ItensPlaylist { get; set; }
    }
}