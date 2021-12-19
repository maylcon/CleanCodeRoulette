using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineBettingRoulette.Repositories.Roulette
{
    public interface IRouletteRepository
    {
        Task<Models.Roulette> Create(Models.Roulette entity);
        Task<List<Models.Roulette>> GetAll();
        Task<Models.Roulette> Open(Guid id);
        Task<Models.Roulette> Close(Guid id);
    }
}
