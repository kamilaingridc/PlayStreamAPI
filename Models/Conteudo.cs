namespace PlayStreamAPI.Models
{
    public class Conteudo
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int CriadorId { get; set; }  
        public Criador Criador { get; set; } 
    }

}
