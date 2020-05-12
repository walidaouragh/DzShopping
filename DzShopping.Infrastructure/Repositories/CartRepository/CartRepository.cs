using System;
using System.Text.Json;
using System.Threading.Tasks;
using DzShopping.Core.Models;
using StackExchange.Redis;

namespace DzShopping.Infrastructure.Repositories.CartRepository
{
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase _database;

        public CartRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<CustomerCart> GetCartAsync(string cartId)
        {
            var data = await _database.StringGetAsync(cartId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerCart>(data);
        }

        public async Task<CustomerCart> UpdateCartAsync(CustomerCart cart)
        {
            var created =
                await _database.StringSetAsync(cart.CartId, JsonSerializer.Serialize(cart), TimeSpan.FromDays(30));
            if (!created) return null;
            return await GetCartAsync(cart.CartId);
        }

        public async Task<bool> DeleteCartAsync(string cartId)
        {
            return await _database.KeyDeleteAsync(cartId);
        }
    }
}
