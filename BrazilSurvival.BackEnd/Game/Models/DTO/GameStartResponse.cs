using BrazilSurvival.BackEnd.Challenges.Models;

namespace BrazilSurvival.BackEnd.Game.Models.DTO;

public record GameStartResponse(
    GameStats gameStats,
    List<Challenge> challenges
);
