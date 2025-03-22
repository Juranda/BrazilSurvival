using BrazilSurvival.BackEnd.Auth.Models;
using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.Game.Models;
using BrazilSurvival.BackEnd.PlayersScores.Models;
using Microsoft.EntityFrameworkCore;

namespace BrazilSurvival.BackEnd.Data;

public class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
    
    public DbSet<User> Users { get; set; }
}