using OnlineBettingRoulette.Dtos.Bet;
using System.Threading.Tasks;

namespace OnlineBettingRoulette.Services.Bet
{
    public interface IBetService
    {
        Task<ReadBet> Create(CreateBet request, string user);
    }
}
