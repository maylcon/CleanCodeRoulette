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
            ReadBet readBet = _mapper.Map<CreateBet, ReadBet>(request);
            readBet.User = user;
            Models.Bet bet = _mapper.Map<ReadBet, Models.Bet>(readBet);
            bet = await _repository.Create(bet);
            readBet = _mapper.Map<Models.Bet, ReadBet>(bet);
            return readBet;
        }
    }
}
