using AutoMapper;
using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.CustomActionFilters;
using BrazilSurvival.BackEnd.Errors;
using BrazilSurvival.BackEnd.ExceptionHandlers;
using BrazilSurvival.BackEnd.Game.Models;
using BrazilSurvival.BackEnd.Game.Models.DTO;
using BrazilSurvival.BackEnd.Game.Services;
using Microsoft.AspNetCore.Mvc;

namespace BrazilSurvival.BackEnd.Game;

[Route("[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameService gameService;
    private readonly IMapper mapper;

    public GameController(IGameService gameService, IMapper mapper)
    {
        this.gameService = gameService;
        this.mapper = mapper;
    }

    [HttpPost("start")]
    [ValidateModel]
    public async Task<IActionResult> Start([FromBody] PlayerStatsDTO? request)
    {
        var (playerStats, challenges) = await gameService.StartGame(mapper.Map<PlayerStats>(request));

        return Ok(new GameStartResponse(mapper.Map<PlayerStatsDTO>(playerStats), mapper.Map<List<ChallengeDTO>>(challenges)));
    }

    [HttpPost("next")]
    [ValidateModel]
    public async Task<IActionResult> NextChallenge([FromBody] GameNextChallengeRequest request)
    {
        var result = await gameService.AnswerChallenge(mapper.Map<PlayerStats>(request.PlayerStats), request.ChallengeId, request.OptionId, request.RequestNewChallenges ?? false);

        if (result.HasError)
        {
            return ErrorResponse.NotFound(result.Error.Message);
        }

        AnswerChallengeResult answerChallengeResult = result.Value;

        return Ok(mapper.Map<AnswerChallengeResultDTO>(answerChallengeResult));
    }
}
