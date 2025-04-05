using AutoMapper;
using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.CustomActionFilters;
using BrazilSurvival.BackEnd.Errors;
using BrazilSurvival.BackEnd.ExceptionHandlers;
using BrazilSurvival.BackEnd.Game.Models;
using BrazilSurvival.BackEnd.Game.Models.DTO;
using BrazilSurvival.BackEnd.Game.Services;
using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public async Task<IActionResult> Start([FromBody] PlayerStatsDTO? request)
    {
        var (token, playerStats, challenges) = await gameService.StartGame(mapper.Map<PlayerStats>(request));

        return Ok(new GameStartResponse(token, mapper.Map<PlayerStatsDTO>(playerStats), mapper.Map<List<ChallengeDTO>>(challenges)));
    }

    [HttpPost("next")]
    [ValidateModel]
    [AllowAnonymous]
    public async Task<IActionResult> NextChallenge([FromBody] GameNextChallengeRequest request)
    {
        var result = await gameService.AnswerChallenge(request.Token, request.ChallengeId, request.OptionId, request.RequestNewChallenges ?? false);

        if (result.HasError)
        {
            return ErrorResponse.FromError(result.Error);
        }

        AnswerChallengeResult answerChallengeResult = result.Value;

        return Ok(mapper.Map<AnswerChallengeResultDTO>(answerChallengeResult));
    }
}
