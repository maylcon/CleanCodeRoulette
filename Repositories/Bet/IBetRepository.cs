using System.Threading.Tasks;

namespace OnlineBettingRoulette.Repositories.Bet
{
    public interface IBetRepository
    {
        Task<Models.Bet> Create(Models.Bet entity);
    }
}
