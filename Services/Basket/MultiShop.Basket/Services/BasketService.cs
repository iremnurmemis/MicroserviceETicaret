using MultiShop.Basket.Dtos;
using MultiShop.Basket.Settings;
using System.Text.Json;

namespace MultiShop.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task DeleteBasket(string userId)
        {
            await _redisService.GetDb().KeyDeleteAsync(userId);
        }

        public async Task<BasketTotalDto?> GetBasket(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty", nameof(userId));
            }

            var existBasket = await _redisService.GetDb().StringGetAsync(userId);

            if (existBasket.IsNullOrEmpty)
            {
                return null; // Eğer Redis'te veri yoksa null dönebiliriz
            }

            return JsonSerializer.Deserialize<BasketTotalDto>(existBasket.ToString());
        }


        public async Task SaveBasket(BasketTotalDto basketTotalDto)
        {
            await _redisService.GetDb().StringSetAsync(basketTotalDto.UserId,JsonSerializer.Serialize(basketTotalDto));
        }
    }
}
