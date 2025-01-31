using AutoMapper;
using BrazilSurvival.BackEnd.Challenges;
using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.CustomActionFilters;
using BrazilSurvival.BackEnd.Game.Models.DTO;
using BrazilSurvival.BackEnd.PlayersScores;
using Microsoft.AspNetCore.Mvc;

namespace BrazilSurvival.BackEnd.Game;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly GameService gameService;
    private readonly IMapper mapper;

    public GameController(GameService gameService, IMapper mapper)
    {
        this.gameService = gameService;
        this.mapper = mapper;
    }

    [HttpPost("start")]
    [ValidateModel]
    public async Task<IActionResult> Start([FromBody] GameStartRequest request)
    {
        var result = await gameService.StartGame(request.GameStats);

        return Ok(new GameStartResponse(result.Item1, result.Item2));
    }

    [HttpPost("next")]
    [ValidateModel]
    public async Task<IActionResult> NextChallenge([FromBody] GameNextChallengeRequest request)
    {
        var result = await gameService.NextChallenge(request.GameStats, request.ChallengeId, request.OptionId, request.RequestNewChallenges ?? false);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<AnswerChallengeResultDTO>(result));
    }
}
