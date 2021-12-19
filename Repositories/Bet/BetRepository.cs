using StackExchange.Redis;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineBettingRoulette.Repositories.Bet
{
    public class BetRepository : IBetRepository
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly string _keyTable = "bets:";

        public BetRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<Models.Bet> Create(Models.Bet entity)
        {
            var database = _redis.GetDatabase();
            var entityToBytes = JsonSerializer.SerializeToUtf8Bytes(entity);
            await database.StringSetAsync(_keyTable + entity.Id, entityToBytes);
            var getRoulette = await database.StringGetAsync(_keyTable + entity.Id.ToString());
            var bytesToEntity = JsonSerializer.Deserialize<Models.Bet>(getRoulette);
            return bytesToEntity;
        }
    }
}
