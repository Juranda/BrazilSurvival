using BrazilSurvival.BackEnd.Challenges.Models;

namespace BrazilSurvival.BackEnd.Game.Models.DTO;

public record GameStartResponse(
    PlayerStatsDTO PlayerStats,
    List<ChallengeDTO> Challenges
);
