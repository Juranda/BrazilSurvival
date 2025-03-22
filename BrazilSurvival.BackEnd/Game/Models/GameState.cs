using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrazilSurvival.BackEnd.Game.Models;

public class GameState
{
    [Column(TypeName = "CHAR(36)")]
    public Guid Token { get; set; }
    public int Health { get; set; }
    public int Money { get; set; }
    public int Power { get; set; }
    public int Score { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    
    // Evita checks
    public bool IsOver => EndedAt is not null;
}