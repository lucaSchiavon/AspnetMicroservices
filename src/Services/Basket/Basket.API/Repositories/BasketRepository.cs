using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository:IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var Basket = await _redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(Basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(Basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            //con questo metodo si puo rimuovere aggiungere o modificare...
            await _redisCache.SetStringAsync(basket.Username, JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.Username);
        }
    }
}
