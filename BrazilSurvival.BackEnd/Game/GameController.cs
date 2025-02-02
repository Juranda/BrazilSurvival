using AutoMapper;
using BrazilSurvival.BackEnd.Challenges;
using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.CustomActionFilters;
using BrazilSurvival.BackEnd.Game.Exceptions;
using BrazilSurvival.BackEnd.Game.Models;
using BrazilSurvival.BackEnd.Game.Models.DTO;
using BrazilSurvival.BackEnd.PlayersScores;
using Microsoft.AspNetCore.Mvc;

namespace BrazilSurvival.BackEnd.Game;

[Route("[controller]")]
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
    public async Task<IActionResult> Start([FromBody] PlayerStatsDTO? request)
    {
        try
        {
            var result = await gameService.StartGame(mapper.Map<PlayerStats>(request));
            return Ok(new GameStartResponse(mapper.Map<PlayerStatsDTO>(result.Item1), mapper.Map<List<ChallengeDTO>>(result.Item2)));
        }
        catch (NotFoundException e)
        {
            throw new ProcessException(e.Message, null ,StatusCodes.Status404NotFound);
        }
    }

    [HttpPost("next")]
    [ValidateModel]
    public async Task<IActionResult> NextChallenge([FromBody] GameNextChallengeRequest request)
    {
        try
        {
            var result = await gameService.NextChallenge(mapper.Map<PlayerStats>(request.PlayerStats), request.ChallengeId, request.OptionId, request.RequestNewChallenges ?? false);
            return Ok(mapper.Map<AnswerChallengeResultDTO>(result));
        }
        catch (NotFoundException e)
        {
            throw new ProcessException(e.Message, null, StatusCodes.Status404NotFound);
        }
    }
}
