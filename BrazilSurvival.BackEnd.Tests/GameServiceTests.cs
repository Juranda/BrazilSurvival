using BrazilSurvival.BackEnd.Challenges;
using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.Game.Models;
using BrazilSurvival.BackEnd.Game.Repo;
using BrazilSurvival.BackEnd.Game.Services;
using Moq;

namespace BrazilSurvival.BackEnd.Tests;

public class GameServiceTests
{
    [Fact]
    public async Task StartWithNullPlayerStats_ShoudReturnNewGameWith10ForEveryStat()
    {
        // Arrange
        List<Challenge> challengesMoq = [
            new() {
                Id = 1,
            },
            new() {
                Id = 2,
            },
            new() {
                Id = 3,
            },
        ];


        var challengeRepoMoq = new Mock<IChallengeRepo>();
        challengeRepoMoq.Setup(r => r.GetChallengesAsync(It.IsAny<int>())).ReturnsAsync(challengesMoq);

        var gameStateRepoMoq = new Mock<IGameStateRepo>();
        gameStateRepoMoq.Setup(r => r.PostGameState(It.IsAny<GameState>())).ReturnsAsync(new GameState() {
            Token = Guid.NewGuid()
        });

        GameService gameService = new(challengeRepoMoq.Object, gameStateRepoMoq.Object);

        // Act
        var (guid, stats, challenges) = await gameService.StartGame(null);

        // Assert
        Assert.NotEmpty(challenges);
        Assert.Equal(10, stats.Health);
        Assert.Equal(10, stats.Money);
        Assert.Equal(10, stats.Power);
    }
}
