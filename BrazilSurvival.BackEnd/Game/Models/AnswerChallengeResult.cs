using BrazilSurvival.BackEnd.Challenges.Models;

namespace BrazilSurvival.BackEnd.Game.Models;

public record AnswerChallengeResult(
    Guid Token,
    string Answer,
    string Consequence,
    AnswerEffect Effect,
    PlayerStats NewPlayerStats,
    bool IsGameOver,
    List<Challenge>? NewChallenges
);
