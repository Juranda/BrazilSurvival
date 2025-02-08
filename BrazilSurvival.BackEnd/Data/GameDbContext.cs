using BrazilSurvival.BackEnd.Challenges.Models;
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
        
        modelBuilder.Entity<Challenge>()
            .HasMany(c => c.Options)
            .WithOne(o => o.Challenge)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ChallengeOption>()
            .HasMany(co => co.Consequences)
            .WithOne(con => con.ChallengeOption)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<Challenge> Challenges { get; set; }
    public DbSet<PlayerScore> PlayerScores { get; set; }
}