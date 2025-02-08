using System.ComponentModel.DataAnnotations;

namespace BrazilSurvival.BackEnd.Challenges.Models.DTO;

public class PostChallengeRequest
{
    [Required]
    [StringLength(255, MinimumLength = 10)]
    public required string Title { get; set; }
    [Required]
    [MinLength(4)]
    public required List<PostChallengeOption> Options { get; set; }
}
