using AutoMapper; 
using OnlineBettingRoulette.Dtos.Bet;
using OnlineBettingRoulette.Repositories.Bet;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineBettingRoulette.Services.Bet
{
    public class BetService : IBetService
    {
        private readonly IBetRepository _repository;
        private readonly IMapper _mapper;
        private const string _ROJO = "rojo";
        private const string _NEGRO = "negro";
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

        public async Task<List<WinBet>> Close(Guid idRoulette)
        {
            var bet = await _repository.Close(idRoulette);
            var readBet = _mapper.Map<List<Models.Bet>, List<ReadBet>>(bet);
            var listWin = Calculate(readBet);
            return listWin;
        }

        List<WinBet> Calculate(List<ReadBet> bets)
        {
            Random generateNumberRandom = new Random();
            int numberWin = generateNumberRandom.Next(37);
            var modWin = numberWin % 2;
            var listWin = new List<WinBet>();
            foreach (var bet in bets)
            {
                if ((bet.NumberColor == _NEGRO && modWin != 0 && bet.NumberColor != "0")|| (bet.NumberColor == _ROJO && modWin == 0 && bet.NumberColor != "0"))
                {
                    listWin.Add(new WinBet { Bet = bet, Profit = 1.8M * Decimal.Parse(bet.Value),NumberWin = numberWin });
                    continue;
                }
                if (numberWin.ToString() == bet.NumberColor)
                {
                    listWin.Add(new WinBet{ Bet = bet , Profit = 5M * Decimal.Parse(bet.Value), NumberWin = numberWin });
                    continue;
                }
            }
            return listWin;
        }
    }
}
