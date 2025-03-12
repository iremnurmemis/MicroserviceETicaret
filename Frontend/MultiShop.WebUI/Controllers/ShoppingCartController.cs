using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.BasketDtos;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;

namespace MultiShop.WebUI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;
        public ShoppingCartController(IProductService productService, IBasketService basketService)
        {
            _productService = productService;
            _basketService = basketService;
        }


        public async Task<IActionResult> Index(string code,int discountRate,decimal totalPriceWithDiscount)
        {
            ViewBag.code=code;
            ViewBag.discountRate=discountRate;
            var values = await _basketService.GetBasket();
            ViewBag.total = values.TotalPrice;
            var totalPriceWithTax = values.TotalPrice + values.TotalPrice / 100 * 10;
            ViewBag.totalPriceWithTax=totalPriceWithTax;
            var kdv= values.TotalPrice / 100 * 10;
            ViewBag.kdv=kdv;
            ViewBag.totalPriceWithDiscount = totalPriceWithDiscount;

            if (totalPriceWithDiscount > 0)
            {
                ViewBag.totalPriceWithDiscount = totalPriceWithDiscount;
            }
            else
            {
                ViewBag.totalPriceWithDiscount = totalPriceWithTax; // Kupon uygulanmadıysa, eski fiyatı göster
            }

            return View();
        }

        public async Task<IActionResult> AddBasketItem(string id)
        {
            var values = await _productService.GetByIdProductAsync(id);
            var items = new BasketItemDto
            {
                ProductId = id,
                Price = values.ProductPrice,
                ProductName = values.ProductName,
                ProductImageUrl=values.ProductImageUrl,
                Quantity = 1
            };

            await _basketService.AddBasketItem(items);
            return RedirectToAction("Index");
        }

       
        public async Task<IActionResult> RemoveBasketItem(string id)
        {
            await _basketService.RemoveBasketItem(id);
            return RedirectToAction("Index");

        }

    }
}
