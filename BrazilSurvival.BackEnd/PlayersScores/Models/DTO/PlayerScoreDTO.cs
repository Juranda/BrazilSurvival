namespace BrazilSurvival.BackEnd.PlayersScores.Models.DTO;

public class PlayerScoreDTO {
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Score { get; set; }
}
