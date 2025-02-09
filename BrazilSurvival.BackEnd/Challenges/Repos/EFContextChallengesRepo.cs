using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.Data;
using BrazilSurvival.BackEnd.Errors;
using Microsoft.EntityFrameworkCore;

namespace BrazilSurvival.BackEnd.Challenges.Repos;

public class EFContextChallengesRepo : IChallengeRepo
{
    private readonly GameDbConext context;
    public EFContextChallengesRepo(GameDbConext context)
    {
        this.context = context;
    }
    public async Task<List<Challenge>> GetChallengesAsync(int quantity = 10)
    {
        return await context.Challenges
            .Take(quantity)
            .Include(c => c.Options)
            .ThenInclude(o => o.Consequences)
            .ToListAsync();
    }

    public async Task<Result<Challenge>> GetChallengeAsync(int id)
    {
        Challenge? challenge = await context.Challenges
            .Include(c => c.Options)
            .ThenInclude(o => o.Consequences)
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();


        if (challenge == null)
        {
            return Error.NotFound();
        }

        return challenge;
    }


    public async Task<Result<Challenge>> PostChallengeAsync(Challenge challenge)
    {
        // Talvez validar o desafio, por isso o result

        await context.Challenges.AddAsync(challenge);
        await context.SaveChangesAsync();
        return challenge;
    }
}
