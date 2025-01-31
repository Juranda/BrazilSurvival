using System.ComponentModel.DataAnnotations;

namespace BrazilSurvival.BackEnd.Game.Models.DTO;

public record GameNextChallengeRequest(
    [Required]
    GameStats gameStats,
    [Required]
    int ChallengeId,
    [Required]
    int AnswerId
);
