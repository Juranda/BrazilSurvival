namespace BrazilSurvival.BackEnd.Models.DTO;
using System.ComponentModel.DataAnnotations;


public record PlayerScorePostRequest(
    [Required]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "Name should have 6 characters")]
    string Name,

    [Required]
    int Score
);