using MultiShop.DtoLayer.BasketDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddBasketItem(BasketItemDto basketItemDto)
        {
            var currentBasket = await GetBasket();
            if (currentBasket is null)
            {
                currentBasket = new BasketTotalDto();
                currentBasket.BasketItems.Add(basketItemDto);
            }
            else
            {
                var searchedBasketItem = currentBasket.BasketItems.FirstOrDefault(x => x.ProductId == basketItemDto.ProductId);
                if (searchedBasketItem is not null)
                {
                    searchedBasketItem.Quantity += 1;
                }
                else
                {
                    currentBasket.BasketItems.Add(basketItemDto);
                }
            }

            await SaveBasket(currentBasket);
        }


        public async Task DeleteBasket(string userId)
        {
            await _httpClient.DeleteAsync(userId);
        }

        public async Task<BasketTotalDto> GetBasket()
        {
            var responseMessage = await _httpClient.GetAsync("Baskets");
            var values = await responseMessage.Content.ReadFromJsonAsync<BasketTotalDto>();
            return values;
        }

        public async Task<bool> RemoveBasketItem(string productId)
        {
            var values = await GetBasket();
            var deletedItem = values.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            var result = values.BasketItems.Remove(deletedItem);
            await SaveBasket(values);
            return true;
        }


        public async Task SaveBasket(BasketTotalDto basketTotalDto)
        {
            await _httpClient.PostAsJsonAsync<BasketTotalDto>("Baskets",basketTotalDto);
        }

    }
}
