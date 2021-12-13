using AutoMapper;
using OnlineBettingRoulette.Dtos.Roulette;

namespace OnlineBettingRoulette.Mappers.Roulette
{
    public class CreateRouletteMapper : Profile
    {
        public CreateRouletteMapper() {
            CreateMap<CreateRoulette, Models.Roulette>();
        }
    }
}
