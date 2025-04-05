using AutoMapper;
using BrazilSurvival.BackEnd.CustomActionFilters;
using BrazilSurvival.BackEnd.Errors;
using BrazilSurvival.BackEnd.PlayersScores.Models;
using BrazilSurvival.BackEnd.PlayersScores.Models.DTO;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> GetPlayersScores([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var playerScores = await playerScoreRepo.GetPlayerScoresAsync(page, pageSize);
        return Ok(mapper.Map<List<PlayerScoreDTO>>(playerScores));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlayerScore(int id)
    {
        var result = await playerScoreRepo.GetPlayerScoreAsync(id);

        if (result.HasError)
        {
            return NotFound();
        }

        var playerScore = result.Value;

        return Ok(mapper.Map<PlayerScoreDTO>(playerScore));
    }

    [HttpPost]
    [ValidateModel]
    [Authorize(AuthorizationPolicies.PLAYER)]
    public async Task<IActionResult> PostNewPlayerScore([FromBody] PlayerScorePostRequest request)
    {
        Result<PlayerScore> result = await playerScoreRepo.PostPlayerScoreAsync(request.Token, request.Name);

        if (result.HasError)
        {
            return ErrorResponse.FromError(result.Error);
        }
        
        PlayerScore playerScore = result.Value;

        return CreatedAtAction(nameof(GetPlayerScore), new { playerScore.Id }, mapper.Map<PlayerScoreDTO>(playerScore));
    }
}
