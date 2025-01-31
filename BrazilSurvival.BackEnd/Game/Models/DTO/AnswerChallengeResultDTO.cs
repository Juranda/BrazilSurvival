using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.Game.Models.DTO;

namespace BrazilSurvival.BackEnd.Game;

public record AnswerChallengeResultDTO(
    string Answer,
    string Consequence,
    GameStatsDTO NewGameStats,
    bool IsGameOver,
    List<ChallengeDTO>? NewChallenges
); 