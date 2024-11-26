using PlayStreamAPI.Models;
public class Criador
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    public ICollection<Conteudo> Conteudos { get; set; }  
}
