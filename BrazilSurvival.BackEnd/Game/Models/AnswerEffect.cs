using System.ComponentModel.DataAnnotations;

namespace BrazilSurvival.BackEnd.Game.Models;

public record AnswerEffect(
    [Required]
    int Health = 10,
    [Required]
    int Money = 10,
    [Required]
    int Power = 10
);
