using BrazilSurvival.BackEnd.Challenges.Models;

namespace BrazilSurvival.BackEnd.Game.Models;

public record AnswerChallengeResult(
    string Answer,
    string Consequence,
    PlayerStats NewPlayerStats,
    bool IsGameOver,
    List<Challenge>? NewChallenges
); 