using AutoMapper;
using OnlineBettingRoulette.Dtos.Bet;

namespace OnlineBettingRoulette.Mappers.Bet
{
    public class CreateBetMapper : Profile
    {
        public CreateBetMapper()
        {
            CreateMap<CreateBet, Models.Bet>();
            CreateMap<CreateBet, ReadBet>();
        }
    }
}
