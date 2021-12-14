using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineBettingRoulette.Repositories.Roulette
{
    public class RouletteRepository : IRouletteRepository
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly string _keyTable = "roulettes:";

        public RouletteRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<Models.Roulette> Create (Models.Roulette entity)
        {
            var database = _redis.GetDatabase();
            var entityToBytes = JsonSerializer.SerializeToUtf8Bytes(entity);
            await database.StringSetAsync(_keyTable+ entity.Id, entityToBytes);
            var getRoulette = await database.StringGetAsync(_keyTable + entity.Id.ToString());
            var bytesToEntity = JsonSerializer.Deserialize<Models.Roulette>(getRoulette);
            return bytesToEntity;
        }

        public async Task<List<Models.Roulette>> GetAll()
        {
            var database = _redis.GetDatabase();
            EndPoint endPoint = _redis.GetEndPoints().First();
            RedisKey[] keys = _redis.GetServer(endPoint).Keys(pattern: _keyTable+"*").ToArray();
            var listRoulette = new List<Models.Roulette>();
            for (int i =0; i < keys.Count(); i++)
            {
                var getRoulette = await database.StringGetAsync(keys[i]);
                var bytesToEntity = JsonSerializer.Deserialize<Models.Roulette>(getRoulette);
                listRoulette.Add(bytesToEntity);
            }
            return listRoulette;
        }

        public async Task<Models.Roulette> Open(Guid id)
        {
            var database = _redis.GetDatabase();
            var getRoulette = await database.StringGetAsync(_keyTable + id.ToString());
            if (getRoulette.IsNull) {
                return null;
            }
            var bytesToEntity = JsonSerializer.Deserialize<Models.Roulette>(getRoulette);
            bytesToEntity.Estado = "abierta";
            var entityToBytes = JsonSerializer.SerializeToUtf8Bytes(bytesToEntity);
            await database.StringSetAsync(_keyTable + bytesToEntity.Id, entityToBytes);
            return bytesToEntity;

        }

    }
}
