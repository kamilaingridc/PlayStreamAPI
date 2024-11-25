using Microsoft.EntityFrameworkCore;
using PlayStreamAPI.Models; 

namespace PlayStreamAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Conteudo> Conteudos { get; set; }
        public DbSet<Criador> Criadores { get; set; }
        public DbSet<ItemPlaylist> ItemPlaylists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Playlist>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Playlist)
                .HasForeignKey(p => p.UsuarioId);

            modelBuilder.Entity<Conteudo>()
                .HasOne(c => c.Criador)
                .WithMany(cr => cr.Conteudo)
                .HasForeignKey(c => c.CriadorId);

            modelBuilder.Entity<ItemPlaylist>()
                .HasKey(ip => new { ip.PlaylistId, ip.ConteudoId });

            modelBuilder.Entity<ItemPlaylist>()
                .HasOne(ip => ip.Playlist)
                .WithMany(p => p.ItemPlaylist)
                .HasForeignKey(ip => ip.PlaylistId);

            modelBuilder.Entity<ItemPlaylist>()
                .HasOne(ip => ip.Conteudo)
                .WithMany(c => c.Itens)
                .HasForeignKey(ip => ip.ConteudoId);
        }
    }
}
