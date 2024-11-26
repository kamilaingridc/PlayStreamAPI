using PlayStreamAPI.Models;

public class ItemPlaylist
{
    public int PlaylistId { get; set; }  
    public int ConteudoId { get; set; }   

    public Playlist Playlist { get; set; } = new Playlist();
    public Conteudo Conteudo { get; set; } = new Conteudo();
}
