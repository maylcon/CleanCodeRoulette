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
        private readonly string _KEYTABLE = "roulettes:";
        private const string _ESTADOABIERTA = "abierta";
        private const string _ESTADOCERRADA = "cerrada";

        public RouletteRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<Models.Roulette> Create (Models.Roulette entity)
        {
            var database = _redis.GetDatabase();
            var entityToBytes = JsonSerializer.SerializeToUtf8Bytes(entity);
            await database.StringSetAsync(_KEYTABLE+ entity.Id, entityToBytes);
            var getRoulette = await database.StringGetAsync(_KEYTABLE + entity.Id.ToString());
            var bytesToEntity = JsonSerializer.Deserialize<Models.Roulette>(getRoulette);
            return bytesToEntity;
        }

        public async Task<List<Models.Roulette>> GetAll()
        {
            var database = _redis.GetDatabase();
            EndPoint endPoint = _redis.GetEndPoints().First();
            RedisKey[] keys = _redis.GetServer(endPoint).Keys(pattern: _KEYTABLE+"*").ToArray();
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
            var getRoulette = await database.StringGetAsync(_KEYTABLE + id.ToString());
            if (getRoulette.IsNull) {
                return null;
            }
            var bytesToEntity = JsonSerializer.Deserialize<Models.Roulette>(getRoulette);
            bytesToEntity.Estado = _ESTADOABIERTA;
            var entityToBytes = JsonSerializer.SerializeToUtf8Bytes(bytesToEntity);
            await database.StringSetAsync(_KEYTABLE + bytesToEntity.Id, entityToBytes);
            return bytesToEntity;

        }

        public async Task<Models.Roulette> Close(Guid id)
        {
            var database = _redis.GetDatabase();
            var getRoulette = await database.StringGetAsync(_KEYTABLE + id.ToString());
            if (getRoulette.IsNull)
            {
                return null;
            }
            var bytesToEntity = JsonSerializer.Deserialize<Models.Roulette>(getRoulette);
            if (bytesToEntity.Estado == _ESTADOCERRADA)
            {
                return null;
            }
            bytesToEntity.Estado = _ESTADOCERRADA;
            var entityToBytes = JsonSerializer.SerializeToUtf8Bytes(bytesToEntity);
            await database.StringSetAsync(_KEYTABLE + bytesToEntity.Id, entityToBytes);
            return bytesToEntity;

        }

        public async Task<bool> Exist(Guid id)
        {
            var exist = false;
            var database = _redis.GetDatabase();
            var getRoulette = await database.StringGetAsync(_KEYTABLE + id.ToString());
            if (!getRoulette.IsNull)
            {
                var bytesToEntity = JsonSerializer.Deserialize<Models.Roulette>(getRoulette);
                exist = bytesToEntity.Estado == _ESTADOABIERTA ? true : false;
            }
            return exist;
        }

    }
}
