using System.ComponentModel.DataAnnotations;

namespace BrazilSurvival.BackEnd.Challenges.Models;

public class Challenge
{
    public int Id { get; set; }
    [StringLength(255)]
    public string Title { get; set; } = "";
    public List<ChallengeOption> Options { get; set; } = [];
}