using PlayStreamAPI.Models;
using PlayStreamAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace PlayStreamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly PlaylistRepository _playlistRepository;

        public PlaylistController(PlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        // GET: api/playlist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetAllPlaylists()
        {
            var playlists = await _playlistRepository.GetAllPlaylistsAsync();
            return Ok(playlists);
        }

        // POST: api/playlist
        [HttpPost]
        public async Task<ActionResult<Playlist>> CreatePlaylist([FromBody] Playlist playlist)
        {
            await _playlistRepository.AddPlaylistAsync(playlist);
            return CreatedAtAction(nameof(GetAllPlaylists), new { id = playlist.Id }, playlist);
        }

        // PUT: api/playlist/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlaylist(int id, [FromBody] Playlist playlist)
        {
            if (id != playlist.Id)
            {
                return BadRequest();
            }

            await _playlistRepository.UpdatePlaylistAsync(playlist);
            return NoContent();
        }

        // DELETE: api/playlist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            await _playlistRepository.DeletePlaylistAsync(id);
            return NoContent();
        }
    }
}
