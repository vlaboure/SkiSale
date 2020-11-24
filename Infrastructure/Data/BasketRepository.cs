using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket> GetBasketAsync(string id)
        {
            var data = await _database.StringGetAsync(id);
            return data.IsNullOrEmpty ? null 
                : JsonSerializer.Deserialize<CustomerBasket>(data); 
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var data = await _database.StringSetAsync(basket.Id,
                                    //TimeSpan.FromDays --> fixe à un nombre de jours
                JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if(!data) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}