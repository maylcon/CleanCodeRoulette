using OnlineBettingRoulette.Dtos.Roulette;
using System.Threading.Tasks;

namespace OnlineBettingRoulette.Services.Roulette
{
    public interface IRouletteService
    {
        Task<ReadRoulette> Create(CreateRoulette request);
    }
}
