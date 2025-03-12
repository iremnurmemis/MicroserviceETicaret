using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.ProductServices
{
    public class ProductService:IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            await _httpClient.PostAsJsonAsync<CreateProductDto>("Products", createProductDto);

        }

        public async Task DeleteProductAsync(string id)
        {
            await _httpClient.DeleteAsync("Products?id=" + id);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Products");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
            return values;
        }

        public async Task<ResultProductDto> GetByIdProductAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("Products/" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<ResultProductDto>();
            return values;
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Products");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData);
            return values;
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryByCatetegoryIdAsync(string CategoryId)
        {
            var responseMessage = await _httpClient.GetAsync($"Products/ProductListWithCategoryByCategoryId/{CategoryId}");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData);
            return values;
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateProductDto>("Products", updateProductDto);
        }
    }
}
