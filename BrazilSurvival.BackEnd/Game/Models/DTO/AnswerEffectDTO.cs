using System.ComponentModel.DataAnnotations;

namespace BrazilSurvival.BackEnd.Game.Models;

public record AnswerEffectDTO(
    [Required]
    int Health = 10,
    [Required]
    int Money = 10,
    [Required]
    int Power = 10
);