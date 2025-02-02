using AutoMapper;
using BrazilSurvival.BackEnd.Challenges.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrazilSurvival.BackEnd.Challenges;

[Route("[controller]")]
[ApiController]
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
    public async Task<IActionResult> GetChallenges()
    {
        var challenges = await challengeRepo.GetChallengesAsync();

        var challengesDTO = mapper.Map<List<ChallengeDTO>>(challenges);

        return Ok(challengesDTO);
    }
}
