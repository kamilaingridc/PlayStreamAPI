using PlayStreamAPI.Data;
using PlayStreamAPI.Models;

namespace PlayStreamAPI.Controllers
{
    public class PlaylistRepository
    {
        private readonly ApplicationDbContext _context;

        public PlaylistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Playlist>> GetAllPlaylistsAsync()
        {
            return await _context.Playlists
                .Include(p => p.Usuario) 
                .ToListAsync();
        }

        public async Task<Playlist> GetPlaylistByIdAsync(int id)
        {
            return await _context.Playlists
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddPlaylistAsync(Playlist playlist)
        {
            await _context.Playlists.AddAsync(playlist);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePlaylistAsync(Playlist playlist)
        {
            _context.Playlists.Update(playlist);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlaylistAsync(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist != null)
            {
                _context.Playlists.Remove(playlist);
                await _context.SaveChangesAsync();
            }
        }
    }

}
