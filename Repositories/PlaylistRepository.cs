using Microsoft.EntityFrameworkCore;
using PlayStreamAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayStreamAPI.Repositories
{
    public class PlaylistRepository
    {
        private readonly ApplicationDbContext _context;

        public PlaylistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Criar uma nova playlist
        public async Task<Playlist> CreatePlaylistAsync(Playlist playlist)
        {
            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();
            return playlist;
        }

        // Obter todas as playlists
        public async Task<List<Playlist>> GetAllPlaylistsAsync()
        {
            return await _context.Playlists.Include(p => p.Usuario)
                                           .Include(p => p.ItensPlaylist)
                                           .ThenInclude(ip => ip.Conteudo)
                                           .ToListAsync();
        }

        // Obter uma playlist pelo ID
        public async Task<Playlist> GetPlaylistByIdAsync(int id)
        {
            return await _context.Playlists.Include(p => p.Usuario)
                                           .Include(p => p.ItensPlaylist)
                                           .ThenInclude(ip => ip.Conteudo)
                                           .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Atualizar uma playlist
        public async Task<Playlist> UpdatePlaylistAsync(Playlist playlist)
        {
            _context.Playlists.Update(playlist);
            await _context.SaveChangesAsync();
            return playlist;
        }

        // Deletar uma playlist
        public async Task<bool> DeletePlaylistAsync(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
                return false;

            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();
            return true;
        }

        // Adicionar conteúdo à playlist
        public async Task AddConteudoToPlaylistAsync(int playlistId, int conteudoId)
        {
            var itemPlaylist = new ItemPlaylist
            {
                PlaylistId = playlistId,
                ConteudoId = conteudoId
            };

            _context.ItensPlaylist.Add(itemPlaylist);
            await _context.SaveChangesAsync();
        }

        // Remover conteúdo da playlist
        public async Task RemoveConteudoFromPlaylistAsync(int playlistId, int conteudoId)
        {
            var itemPlaylist = await _context.ItensPlaylist
                                              .FirstOrDefaultAsync(ip => ip.PlaylistId == playlistId && ip.ConteudoId == conteudoId);
            if (itemPlaylist != null)
            {
                _context.ItensPlaylist.Remove(itemPlaylist);
                await _context.SaveChangesAsync();
            }
        }
    }
}
