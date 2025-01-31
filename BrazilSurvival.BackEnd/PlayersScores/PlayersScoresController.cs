using BrazilSurvival.BackEnd.CustomActionFilters;
using BrazilSurvival.BackEnd.PlayersScores.Models;
using BrazilSurvival.BackEnd.PlayersScores.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BrazilSurvival.BackEnd.PlayersScores;

[Route("api/[controller]")]
[ApiController]
public class PlayersScoresController : ControllerBase
{
    private readonly IPlayerScoreRepo playerScoreRepo;

    public PlayersScoresController(IPlayerScoreRepo playerScoreRepo)
    {
        this.playerScoreRepo = playerScoreRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlayersScores()
    {
        var playerScore = await playerScoreRepo.GetPlayerScoresAsync();

        if (playerScore == null)
        {
            return NotFound();
        }

        return Ok(playerScore);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlayerScore(int id)
    {
        var playerScore = await playerScoreRepo.GetPlayerScoreAsync(id);

        if (playerScore == null)
        {
            return NotFound();
        }

        return Ok(playerScore);
    }

    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> PostNewPlayerScore([FromBody] PlayerScorePostRequest request)
    {
        var playerScore = new PlayerScore()
        {
            Name = request.Name,
            Score = request.Score
        };

        playerScore = await playerScoreRepo.PostPlayerScoreAsync(playerScore);

        return CreatedAtAction(nameof(GetPlayerScore), new { playerScore.Id }, playerScore);
    }
}
