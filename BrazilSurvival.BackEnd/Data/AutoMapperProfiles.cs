using AutoMapper;
using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.Game;
using BrazilSurvival.BackEnd.Game.Models.DTO;

namespace BrazilSurvival.BackEnd.Data;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Challenge, ChallengeDTO>();
        CreateMap<ChallengeOption, ChallengeOptionDTO>();
        CreateMap<AnswerChallengeResult, AnswerChallengeResultDTO>();
        CreateMap<GameStats, GameStatsDTO>();
    }
}