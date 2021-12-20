using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineBettingRoulette.Repositories.Bet
{
    public interface IBetRepository
    {
        Task<Models.Bet> Create(Models.Bet entity);
        Task<List<Models.Bet>> Close(Guid idRoulette);
    }
}
