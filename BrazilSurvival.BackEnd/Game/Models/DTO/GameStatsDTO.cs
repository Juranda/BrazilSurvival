using System.ComponentModel.DataAnnotations;

namespace BrazilSurvival.BackEnd.Game.Models.DTO;

public record GameStatsDTO(
    [Required]
    int Health = 10,
    [Required]
    int Money = 10,
    [Required]
    int Power = 10
);
