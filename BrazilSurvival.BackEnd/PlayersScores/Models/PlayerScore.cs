using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BrazilSurvival.BackEnd.Game.Models;

namespace BrazilSurvival.BackEnd.PlayersScores.Models;

public class PlayerScore {
    public int Id { get; set; }
    [StringLength(6)]
    public required string Name { get; set; }
    public int Score { get; set; }
    [Column(TypeName = "CHAR(36)")]
    public Guid GameStateToken { get; set; }
    public required GameState GameState { get; set; }
}
