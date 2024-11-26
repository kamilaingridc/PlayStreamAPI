using Microsoft.EntityFrameworkCore;
using PlayStreamAPI.Models;

public class ApplicationDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<Conteudo> Conteudos { get; set; }
    public DbSet<Criador> Criadores { get; set; }
    public DbSet<ItemPlaylist> ItensPlaylist { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }
}
