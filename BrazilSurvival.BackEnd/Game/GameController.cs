using BrazilSurvival.BackEnd.Challenges;
using BrazilSurvival.BackEnd.CustomActionFilters;
using BrazilSurvival.BackEnd.Game.Models.DTO;
using BrazilSurvival.BackEnd.PlayersScores;
using Microsoft.AspNetCore.Mvc;

namespace BrazilSurvival.BackEnd.Game;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IPlayerScoreRepo playerScoreRepo;
    private readonly GameService gameService;

    public GameController(IPlayerScoreRepo playerScoreRepo, GameService gameService)
    {
        this.playerScoreRepo = playerScoreRepo;
        this.gameService = gameService;
    }

    [HttpPost("start")]
    [ValidateModel]
    public async Task<IActionResult> Start([FromBody] GameStartRequest request)
    {
        var result = await gameService.StartGame(request.gameStats);

        return await Task.FromResult(Ok(new GameStartResponse(result.Item1, result.Item2)));
    }

    [HttpPost("next")]
    [ValidateModel]
    public async Task<IActionResult> NextChallenge([FromBody] GameNextChallengeRequest request)
    {
        var result = await gameService.NextChallenge(request.gameStats, request.ChallengeId, request.AnswerId);
        return await Task.FromResult(Ok(result));
    }
}
