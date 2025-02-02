using System.ComponentModel.DataAnnotations;

namespace BrazilSurvival.BackEnd.Game.Models.DTO;

public record PlayerStatsDTO(
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Health should be greater than 0")]
    int Health = 10,
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Money should be greater than 0")]
    int Money = 10,
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Power should be greater than 0")]
    int Power = 10
);
