using System.Threading.Tasks;

namespace OnlineBettingRoulette.Repositories.Roulette
{
    public interface IRouletteRepository
    {
        Task<Models.Roulette> Create(Models.Roulette entity);
    }
}
