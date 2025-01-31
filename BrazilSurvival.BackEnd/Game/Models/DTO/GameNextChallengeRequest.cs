using System.ComponentModel.DataAnnotations;
using BrazilSurvival.BackEnd.Challenges.Models;

namespace BrazilSurvival.BackEnd.Game.Models.DTO;

public record GameNextChallengeRequest(
    [Required]
    GameStats GameStats,
    [Required]
    int ChallengeId,
    [Required]
    int OptionId,
    bool? RequestNewChallenges = false
);
