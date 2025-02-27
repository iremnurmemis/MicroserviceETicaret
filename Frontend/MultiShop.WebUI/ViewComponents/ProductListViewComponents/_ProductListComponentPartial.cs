using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class _ProductListComponentPartial:ViewComponent
    {

        private readonly IHttpClientFactory _httpClientFactory;
        public _ProductListComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            if (id == null)
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7070/api/Products/");
                 if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonnData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonnData);
                    return View(values);
                }
            }
            else
            {
                var client2 = _httpClientFactory.CreateClient();
                var responseMessage2 = await client2.GetAsync("https://localhost:7070/api/Categories/" + id);
                var jsonnData2 = await responseMessage2.Content.ReadAsStringAsync();
                var values2 = JsonConvert.DeserializeObject<ResultCategoryDto>(jsonnData2);


                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync($"https://localhost:7070/api/Products/ProductListWithCategoryByCategoryId/{id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonnData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonnData);
                    ViewBag.ct = values.Count > 0 ? values2.CategoryName + " " + "Kategorisindeki Ürünler" : values2.CategoryName + " " + "Kategorisinde Henüz Ürün Yok";
                    return View(values);
                }

            }
    
            return View();
        }
    }
}
