using BrazilSurvival.BackEnd.Repos;
using Microsoft.AspNetCore.Mvc;

namespace BrazilSurvival.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengesController : ControllerBase
    {
        private readonly IChallengeRepo challengeRepo;

        public ChallengesController(IChallengeRepo challengeRepo)
        {
            this.challengeRepo = challengeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetChallenges()
        {
            var challenges = await challengeRepo.GetChallengesAsync();

            if (challenges == null)
            {
                return NotFound();
            }

            return Ok(challenges);
        }
    }
}
