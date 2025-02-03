using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.Game.Models;
using BrazilSurvival.BackEnd.Game.Models.DTO;

namespace BrazilSurvival.BackEnd.Game;

public record AnswerChallengeResultDTO(
    string Answer,
    string Consequence,
    AnswerEffectDTO effect,
    PlayerStatsDTO NewPlayerStats,
    bool IsGameOver,
    List<ChallengeDTO>? NewChallenges
); 