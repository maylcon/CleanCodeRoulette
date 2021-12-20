using AutoMapper;
using OnlineBettingRoulette.Dtos.Bet;
using OnlineBettingRoulette.Repositories.Bet;
using System.Threading.Tasks;

namespace OnlineBettingRoulette.Services.Bet
{
    public class BetService : IBetService
    {
        private readonly IBetRepository _repository;
        private readonly IMapper _mapper;
        public BetService(IBetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ReadBet> Create(CreateBet request, string user)
        {
            Models.Bet bet = _mapper.Map<CreateBet, Models.Bet>(request);
            bet.User = user;
            bet = await _repository.Create(bet);
            var readBet = _mapper.Map<Models.Bet, ReadBet>(bet);
            return readBet;
        }
    }
}
