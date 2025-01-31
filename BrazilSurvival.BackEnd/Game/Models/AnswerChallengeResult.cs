using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.Game.Models.DTO;

namespace BrazilSurvival.BackEnd.Game;

public record AnswerChallengeResult(
    string Answer,
    string Consequence,
    GameStats NewGameStats,
    bool IsGameOver,
    List<Challenge>? NewChallenges
); 