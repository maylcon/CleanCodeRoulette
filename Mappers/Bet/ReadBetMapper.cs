using AutoMapper;
using OnlineBettingRoulette.Dtos.Bet;

namespace OnlineBettingRoulette.Mappers.Bet
{
    public class ReadBetMapper : Profile
    {
        public ReadBetMapper()
        {
            CreateMap<Models.Bet, ReadBet>();
            CreateMap<ReadBet, Models.Bet>();
        }
    }
}
