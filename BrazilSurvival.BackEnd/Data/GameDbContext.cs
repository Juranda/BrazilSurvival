using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.Game.Models;
using BrazilSurvival.BackEnd.PlayersScores.Models;
using Microsoft.EntityFrameworkCore;

namespace BrazilSurvival.BackEnd.Data;

public class GameDbConext : DbContext
{
    public GameDbConext(DbContextOptions<GameDbConext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayerScore>()
            .HasIndex(s => s.Name)
            .IsUnique(true);

        modelBuilder.Entity<Challenge>()
            .HasMany(c => c.Options)
            .WithOne(o => o.Challenge)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ChallengeOption>()
            .HasMany(co => co.Consequences)
            .WithOne(con => con.ChallengeOption)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GameState>()
            .HasKey(gs => gs.Token);
        
        modelBuilder.Entity<PlayerScore>()
            .HasOne(p => p.GameState);

        modelBuilder.Entity<GameState>()
            .Property(p => p.Token)
            .HasConversion(t => t.ToString(), t => Guid.Parse(t));
    }

    public DbSet<Challenge> Challenges { get; set; }
    public DbSet<PlayerScore> PlayerScores { get; set; }
    public DbSet<GameState> GameStates { get; set; }
}