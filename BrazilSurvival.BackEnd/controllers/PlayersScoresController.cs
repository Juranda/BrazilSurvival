using BrazilSurvival.BackEnd.Models.Domain;
using BrazilSurvival.BackEnd.Models.DTO;
using BrazilSurvival.BackEnd.Repos;
using Microsoft.AspNetCore.Mvc;

namespace BrazilSurvival.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersScoresController : ControllerBase
    {
        private readonly IPlayerScoreRepo playerScoreRepo;

        public PlayersScoresController(IPlayerScoreRepo playerScoreRepo)
        {
            this.playerScoreRepo = playerScoreRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayersScores()
        {
            var playerScore = await playerScoreRepo.GetPlayerScoresAsync();

            if (playerScore == null)
            {
                return NotFound();
            }

            return Ok(playerScore);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerScore(int id)
        {
            var playerScore = await playerScoreRepo.GetPlayerScoreAsync(id);

            if (playerScore == null)
            {
                return NotFound();
            }

            return Ok(playerScore);
        }

        [HttpPost]
        public async Task<IActionResult> PostNewPlayerScore([FromBody] PlayerScorePostRequest request)
        {
            var playerScore = new PlayerScore()
            {
                Name = request.Name,
                Score = request.Score
            };

            playerScore = await playerScoreRepo.PostPlayerScoreAsync(playerScore);

            return CreatedAtAction(nameof(GetPlayerScore), new { playerScore.Id }, playerScore);
        }
    }
}
