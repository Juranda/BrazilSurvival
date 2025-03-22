using System.ComponentModel.DataAnnotations;

namespace BrazilSurvival.BackEnd.PlayersScores.Models.DTO;

public record PlayerScorePostRequest(
    [Required]
    Guid Token,
    [Required]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "Name should have 6 characters")]
    string Name
);