using BrazilSurvival.BackEnd.Challenges.Models;

namespace BrazilSurvival.BackEnd.Game.Models.DTO;

public record GameStartResponse(
    GameStatsDTO GameStats,
    List<Challenge> Challenges
);
