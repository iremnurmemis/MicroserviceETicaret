using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Controllers
{
    public class ProductListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductListController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public IActionResult Index(string id)
        {
           ViewBag.i=id;
            return View();
        }

        public  IActionResult ProductDetail(string id)
        {
            ViewBag.x=id;
            return View();
        }

        [HttpGet]
        public PartialViewResult AddComment()
        {   
            return PartialView();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDto createCommentDto,string pid)
        {
            createCommentDto.ImageUrl = "https://yt3.ggpht.com/a/AATXAJyk2VmL7NqghohEuPMG3VqdQrP66-UTq98FIQ=s900-c-k-c0xffffffff-no-rj-mo";
            createCommentDto.Rating = 1;
            createCommentDto.CreatedDate =DateTime.Parse(DateTime.Now.ToShortDateString());
            createCommentDto.Status =false;
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCommentDto);
            StringContent content = new StringContent(jsonData,Encoding.UTF8,"application/json");
            var responseMessage = await client.PostAsync("https://localhost:7222/api/Comments/",content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductDetail", "ProductList", new { id = createCommentDto.ProductId });
            }

            return View();
        }
    }

   
}
