using System.ComponentModel.DataAnnotations;

namespace BrazilSurvival.BackEnd.Game.Models.DTO;

public record GameNextChallengeRequest(
    [Required]
    PlayerStatsDTO PlayerStats,
    [Required]
    int ChallengeId,
    [Required]
    int OptionId,
    bool? RequestNewChallenges
);
