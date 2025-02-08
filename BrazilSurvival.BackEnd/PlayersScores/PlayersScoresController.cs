using AutoMapper;
using BrazilSurvival.BackEnd.CustomActionFilters;
using BrazilSurvival.BackEnd.Game.Models.DTO;
using BrazilSurvival.BackEnd.PlayersScores.Models;
using BrazilSurvival.BackEnd.PlayersScores.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BrazilSurvival.BackEnd.PlayersScores;

[Route("[controller]")]
[ApiController]
public class PlayersScoresController : ControllerBase
{
    private readonly IPlayerScoreRepo playerScoreRepo;
    private readonly IMapper mapper;

    public PlayersScoresController(IPlayerScoreRepo playerScoreRepo, IMapper mapper)
    {
        this.playerScoreRepo = playerScoreRepo;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlayersScores()
    {
        var playerScores = await playerScoreRepo.GetPlayerScoresAsync();

        if (playerScores.Count == 0)
        {
            return NotFound();
        }

        return Ok(mapper.Map<List<PlayerScoreDTO>>(playerScores));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlayerScore(int id)
    {
        var playerScore = await playerScoreRepo.GetPlayerScoreAsync(id);

        if (playerScore == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<PlayerScoreDTO>(playerScore));
    }

    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> PostNewPlayerScore([FromBody] PlayerScorePostRequest request)
    {
        var playerScore = new PlayerScore()
        {
            Id = 0,
            Name = request.Name.ToUpper(),
            Score = request.Score
        };

        playerScore = await playerScoreRepo.PostPlayerScoreAsync(playerScore);

        return CreatedAtAction(nameof(GetPlayerScore), new { playerScore.Id }, mapper.Map<PlayerScoreDTO>(playerScore));
    }
}
