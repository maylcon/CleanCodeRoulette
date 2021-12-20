using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineBettingRoulette.Repositories.Bet
{
    public class BetRepository : IBetRepository
    {
        private readonly IConnectionMultiplexer _redis;
        private const string _KEYTABLE = "bets:";
        private const string _ESTADOABIERTA = "abierta";
        private const string _ESTADOCERRADA = "cerrada";

        public BetRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<Models.Bet> Create(Models.Bet entity)
        {
            var database = _redis.GetDatabase();
            var entityToBytes = JsonSerializer.SerializeToUtf8Bytes(entity);
            await database.StringSetAsync(_KEYTABLE + entity.IdRoulette + entity.Id, entityToBytes);
            var getRoulette = await database.StringGetAsync(_KEYTABLE +entity.IdRoulette.ToString()+ entity.Id.ToString());
            var bytesToEntity = JsonSerializer.Deserialize<Models.Bet>(getRoulette);
            return bytesToEntity;
        }

        public async Task<List<Models.Bet>> Close(Guid idRoulette)
        {
            var database = _redis.GetDatabase();
            EndPoint endPoint = _redis.GetEndPoints().First();
            RedisKey[] keys = _redis.GetServer(endPoint).Keys(pattern: _KEYTABLE+ idRoulette.ToString() + "*").ToArray();
            var listBet = new List<Models.Bet>();
            for (int i = 0; i < keys.Count(); i++)
            {
                var getRoulette = await database.StringGetAsync(keys[i]);
                var bytesToEntity = JsonSerializer.Deserialize<Models.Bet>(getRoulette);
                if(bytesToEntity.Estado == _ESTADOABIERTA)
                {
                    listBet.Add(bytesToEntity);
                    bytesToEntity.Estado = _ESTADOCERRADA;
                    var entityToBytes = JsonSerializer.SerializeToUtf8Bytes(bytesToEntity);
                    await database.StringSetAsync(keys[i], entityToBytes);
                }
            }
            var listRouletteOrderByDate = (from bet in listBet orderby bet.Date select bet).ToList();
            return listRouletteOrderByDate;
        }
    }
}
