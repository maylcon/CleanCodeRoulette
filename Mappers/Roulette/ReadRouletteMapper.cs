using AutoMapper;
using OnlineBettingRoulette.Dtos.Roulette;

namespace OnlineBettingRoulette.Mappers.Roulette
{
    public class ReadRouletteMapper : Profile
    {
        public ReadRouletteMapper()
        {
            CreateMap<Models.Roulette, ReadRoulette>();
        }
    }
}
