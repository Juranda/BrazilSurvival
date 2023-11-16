namespace BrazilSurvival.BackEnd.Models.Domain;

public class PlayerScore {
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Score { get; set; }
}
