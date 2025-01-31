using BrazilSurvival.BackEnd.Models;
using BrazilSurvival.BackEnd.PlayersScores.Models;
using Microsoft.EntityFrameworkCore;

namespace BrazilSurvival.BackEnd.Data;

public class GameDbConext : DbContext
{
    public GameDbConext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayerScore>()
            .HasIndex(s => s.Name)
            .IsUnique(true);
    }

    public DbSet<Challenge> Challenges { get; set; }
    public DbSet<ChallengeOption> ChallengeOptions { get; set; }
    public DbSet<PlayerScore> PlayerScores { get; set; }
}