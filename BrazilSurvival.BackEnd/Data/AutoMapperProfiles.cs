using AutoMapper;
using BrazilSurvival.BackEnd.Challenges.Models;
using BrazilSurvival.BackEnd.Game;
using BrazilSurvival.BackEnd.Game.Models;
using BrazilSurvival.BackEnd.Game.Models.DTO;
using BrazilSurvival.BackEnd.PlayersScores.Models;
using BrazilSurvival.BackEnd.PlayersScores.Models.DTO;

namespace BrazilSurvival.BackEnd.Data;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Challenge, ChallengeDTO>();
        CreateMap<ChallengeOption, ChallengeOptionDTO>();
        CreateMap<AnswerChallengeResult, AnswerChallengeResultDTO>();
        CreateMap<PlayerStats, PlayerStatsDTO>().ReverseMap();
        CreateMap<PlayerScore, PlayerScoreDTO>().ReverseMap();
        CreateMap<AnswerEffect, AnswerEffectDTO>();
    }
}