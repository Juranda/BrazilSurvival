using System.ComponentModel.DataAnnotations;

namespace BrazilSurvival.BackEnd.PlayersScores.Models;

public class PlayerScore {
    public required int Id { get; set; }
    [StringLength(6)]
    public required string Name { get; set; }
    public required int Score { get; set; }
}
