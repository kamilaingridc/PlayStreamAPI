using Microsoft.AspNetCore.Mvc;
using PlayStreamAPI.Models;
using PlayStreamAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class PlaylistController : ControllerBase
{
    private readonly PlaylistRepository _playlistRepository;

    // Construtor com injeção de dependência
    public PlaylistController(PlaylistRepository playlistRepository)
    {
        _playlistRepository = playlistRepository;
    }

    // Método POST para criar uma nova playlist
    [HttpPost]
    public async Task<IActionResult> CreatePlaylist([FromBody] Playlist playlist)
    {
        var createdPlaylist = await _playlistRepository.CreatePlaylistAsync(playlist);
        return CreatedAtAction(nameof(GetPlaylistById), new { id = createdPlaylist.Id }, createdPlaylist);
    }

    // Método GET para obter todas as playlists
    [HttpGet]
    public async Task<ActionResult<List<Playlist>>> GetAllPlaylists()
    {
        var playlists = await _playlistRepository.GetAllPlaylistsAsync();
        return Ok(playlists);
    }

    // Método GET para obter uma playlist pelo ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Playlist>> GetPlaylistById(int id)
    {
        var playlist = await _playlistRepository.GetPlaylistByIdAsync(id);
        if (playlist == null)
            return NotFound();
        return Ok(playlist);
    }

    // Método POST para adicionar conteúdo a uma playlist
    [HttpPost("{playlistId}/conteudo/{conteudoId}")]
    public async Task<IActionResult> AddConteudoToPlaylist(int playlistId, int conteudoId)
    {
        await _playlistRepository.AddConteudoToPlaylistAsync(playlistId, conteudoId);
        return NoContent();
    }

    // Método DELETE para remover conteúdo de uma playlist
    [HttpDelete("{playlistId}/conteudo/{conteudoId}")]
    public async Task<IActionResult> RemoveConteudoFromPlaylist(int playlistId, int conteudoId)
    {
        await _playlistRepository.RemoveConteudoFromPlaylistAsync(playlistId, conteudoId);
        return NoContent();
    }
}
