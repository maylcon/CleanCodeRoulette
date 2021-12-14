using OnlineBettingRoulette.Dtos.Roulette;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineBettingRoulette.Services.Roulette
{
    public interface IRouletteService
    {
        Task<ReadRoulette> Create(CreateRoulette request);
        Task<List<ReadRoulette>> GetAll();
        Task<ReadRoulette> Open(Guid id);
    }
}
