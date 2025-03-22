using BrazilSurvival.BackEnd.Challenges.Models;

namespace BrazilSurvival.BackEnd.Game.Models.DTO;

public record GameStartResponse(
    Guid Token,
    PlayerStatsDTO PlayerStats,
    List<ChallengeDTO> Challenges
);
