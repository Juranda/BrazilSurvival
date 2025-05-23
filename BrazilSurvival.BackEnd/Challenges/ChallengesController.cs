using AutoMapper;
using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.Challenges.Models.DTO;
using BrazilSurvival.BackEnd.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrazilSurvival.BackEnd.Challenges;

[Route("[controller]")]
[ApiController]
[Authorize(Roles = AuthorizationPolicies.ADMINISTRATOR_OR_PLAYER)]
public class ChallengesController : ControllerBase
{
    private readonly IChallengeRepo challengeRepo;
    private readonly IMapper mapper;

    public ChallengesController(IChallengeRepo challengeRepo, IMapper mapper)
    {
        this.challengeRepo = challengeRepo;
        this.mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetChallenges()
    {
        List<Challenge> challenges = await challengeRepo.GetChallengesAsync(100);

        var challengesDTO = mapper.Map<List<ChallengeDTO>>(challenges);

        return Ok(challengesDTO);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetChallenge(int id)
    {
        Result<Challenge> result = await challengeRepo.GetChallengeAsync(id);

        if (result.HasError)
        {
            return NotFound();
        }

        Challenge challenge = result.Value;
        ChallengeDTO challengeDTO = mapper.Map<ChallengeDTO>(challenge);

        return Ok(challengeDTO);
    }

    [HttpPost]
    public async Task<IActionResult> PostChallenge([FromBody] PostChallengeRequest request)
    {
        Challenge challenge = mapper.Map<Challenge>(request);
        Result<Challenge> result = await challengeRepo.PostChallengeAsync(challenge);

        if (result.HasError)
        {
            return ErrorResponse.InvalidArgument();
        }

        ChallengeDTO challengeDTO = mapper.Map<ChallengeDTO>(challenge);

        return CreatedAtAction(nameof(GetChallenge), new { id = challengeDTO.Id }, challengeDTO);
    }
}
