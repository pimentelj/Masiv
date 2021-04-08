using AutoMapper;
using Test.Entities;
using Test.Models.Roulette;
using Test.Models.Bet;

namespace Test.Helpers
{
    public class AutoMapperRoulette : Profile
    {
        public AutoMapperRoulette()
        {
            CreateMap<Roulette, RouletteModel>();
            CreateMap<Test.Models.Roulette.CreateModel, Roulette>();
            
            CreateMap<Bet, BetModel>();
            CreateMap<Test.Models.Bet.CreateBetModel, Bet>();
        }
    }
}