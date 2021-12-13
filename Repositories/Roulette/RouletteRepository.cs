using StackExchange.Redis;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineBettingRoulette.Repositories.Roulette
{
    public class RouletteRepository : IRouletteRepository
    {
        private readonly IConnectionMultiplexer _redis;

        public RouletteRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<Models.Roulette> Create (Models.Roulette entity)
        {
            var db = _redis.GetDatabase();
            EndPoint endPoint = _redis.GetEndPoints().First();
            RedisKey[] keys = _redis.GetServer(endPoint).Keys(pattern: "*").ToArray();
            var entityToBytes = JsonSerializer.SerializeToUtf8Bytes(entity);
            db.StringSet(entity.Id.ToString(), entityToBytes);
            var datos = db.StringGet(entity.Id.ToString());
            var bytosToEntity = JsonSerializer.Deserialize<Models.Roulette>(datos);
            return bytosToEntity;
        }

    }
}
