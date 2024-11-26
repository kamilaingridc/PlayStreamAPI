using Microsoft.AspNetCore.Mvc;
using PlayStreamAPI.Models;
using PlayStreamAPI.Repositories;

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
        if (playlist == null)
            return BadRequest("Playlist não pode ser nula.");

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
            return NotFound("Playlist não encontrada.");
        return Ok(playlist);
    }

    // Método PUT para atualizar uma playlist
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlaylist(int id, [FromBody] Playlist playlist)
    {
        if (id != playlist.Id)
            return BadRequest("ID da playlist não corresponde.");

        var updatedPlaylist = await _playlistRepository.UpdatePlaylistAsync(playlist);
        if (updatedPlaylist == null)
            return NotFound("Playlist não encontrada para atualização.");

        return Ok(updatedPlaylist);
    }

    // Método DELETE para excluir uma playlist
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlaylist(int id)
    {
        var deleted = await _playlistRepository.DeletePlaylistAsync(id);
        if (!deleted)
            return NotFound("Playlist não encontrada para exclusão.");

        return NoContent();
    }

    // Método POST para adicionar conteúdo a uma playlist
    [HttpPost("{playlistId}/conteudo/{conteudoId}")]
    public async Task<IActionResult> AddConteudoToPlaylist(int playlistId, int conteudoId)
    {
        var result = await _playlistRepository.AddConteudoToPlaylistAsync(playlistId, conteudoId);
        if (!result)
            return NotFound("Playlist ou conteúdo não encontrado.");
        return NoContent();
    }

    // Método DELETE para remover conteúdo de uma playlist
    [HttpDelete("{playlistId}/conteudo/{conteudoId}")]
    public async Task<IActionResult> RemoveConteudoFromPlaylist(int playlistId, int conteudoId)
    {
        var result = await _playlistRepository.RemoveConteudoFromPlaylistAsync(playlistId, conteudoId);
        if (!result)
            return NotFound("Playlist ou conteúdo não encontrado.");

        return NoContent();
    }
}
