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
            // Verifica se a playlist existe
            var existingPlaylist = await _context.Playlists.FindAsync(playlist.Id);
            if (existingPlaylist == null)
                return null;  // Retorna null caso a playlist não exista

            _context.Entry(existingPlaylist).CurrentValues.SetValues(playlist);
            await _context.SaveChangesAsync();
            return existingPlaylist;
        }

        // Deletar uma playlist
        public async Task<bool> DeletePlaylistAsync(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
                return false;  // Retorna false se a playlist não for encontrada

            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();
            return true;
        }

        // Adicionar conteúdo à playlist
        public async Task<bool> AddConteudoToPlaylistAsync(int playlistId, int conteudoId)
        {
            // Verifica se a playlist e conteúdo existem
            var playlist = await _context.Playlists.FindAsync(playlistId);
            var conteudo = await _context.Conteudos.FindAsync(conteudoId);

            if (playlist == null || conteudo == null)
                return false;  // Retorna false se a playlist ou conteúdo não forem encontrados

            var itemPlaylist = new ItemPlaylist
            {
                PlaylistId = playlistId,
                ConteudoId = conteudoId
            };

            _context.ItensPlaylist.Add(itemPlaylist);
            await _context.SaveChangesAsync();
            return true;  // Retorna true após adicionar o conteúdo à playlist
        }

        // Remover conteúdo da playlist
        public async Task<bool> RemoveConteudoFromPlaylistAsync(int playlistId, int conteudoId)
        {
            var itemPlaylist = await _context.ItensPlaylist
                                              .FirstOrDefaultAsync(ip => ip.PlaylistId == playlistId && ip.ConteudoId == conteudoId);

            if (itemPlaylist == null)
                return false;  // Retorna false se o item não for encontrado

            _context.ItensPlaylist.Remove(itemPlaylist);
            await _context.SaveChangesAsync();
            return true;  // Retorna true após remover o conteúdo
        }
    }
}
