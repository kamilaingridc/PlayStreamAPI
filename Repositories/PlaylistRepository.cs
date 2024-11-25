
    using PlayStreamAPI.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using global::PlayStreamAPI.Data;

    namespace PlayStreamAPI.Repositories
    {
        public class PlaylistRepository
        {
            private readonly ApplicationDbContext _context;

            // Construtor que recebe o contexto do banco de dados
            public PlaylistRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            // Método para pegar todas as playlists
            public async Task<List<Playlist>> GetAllPlaylistsAsync()
            {
                return await _context.Playlist
                    .Include(p => p.Usuario)  // Inclui o usuário associado à playlist
                    .ToListAsync();
            }

            // Método para pegar uma playlist por ID
            public async Task<Playlist> GetPlaylistByIdAsync(int id)
            {
                return await _context.Playlist
                    .Include(p => p.Usuario)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }

            // Método para adicionar uma nova playlist
            public async Task AddPlaylistAsync(Playlist playlist)
            {
                await _context.Playlist.AddAsync(playlist);
                await _context.SaveChangesAsync();
            }

            // Método para atualizar uma playlist existente
            public async Task UpdatePlaylistAsync(Playlist playlist)
            {
                _context.Playlist.Update(playlist);
                await _context.SaveChangesAsync();
            }

            // Método para excluir uma playlist
            public async Task DeletePlaylistAsync(int id)
            {
                var playlist = await _context.Playlist.FindAsync(id);
                if (playlist != null)
                {
                    _context.Playlist.Remove(playlist);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
            
