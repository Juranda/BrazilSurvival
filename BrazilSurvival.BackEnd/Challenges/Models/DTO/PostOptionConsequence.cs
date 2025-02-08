using System.ComponentModel.DataAnnotations;

namespace BrazilSurvival.BackEnd.Challenges.Models.DTO;

public class PostOptionConsequence
{
    [StringLength(70, MinimumLength = 1)]
    public required string Answer { get; set; }
    [StringLength(70, MinimumLength = 1)]
    public required string Consequence { get; set; }
    public int Health { get; set; }
    public int Money { get; set; }
    public int Power { get; set; }
}