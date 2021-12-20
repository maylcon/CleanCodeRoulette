using OnlineBettingRoulette.Dtos.Bet;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineBettingRoulette.Services.Bet
{
    public interface IBetService
    {
        Task<ReadBet> Create(CreateBet request, string user);
        Task<List<WinBet>> Close(Guid idRoulette);
    }
}
