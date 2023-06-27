using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

// StackExchange does not support all the data types and features of Redis. It only supports reading and writing byte arrays.

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            
            if (String.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
            // When an object is deserialized using a JSON serializer like Newtonsoft.Json (also known as Json.NET), the computed properties are typically not recalculated.
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        { // computed values are created on from the post body

            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            // By default a type's properties are serialized in opt-out mode. What that means is that all public fields and properties with getters are automatically serialized to JSON, and fields https://github.com/JamesNK/Newtonsoft.Json/issues/2037

            return await GetBasket(basket.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}
