using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos;
using MultiShop.Discount.Services;

namespace MultiShop.Discount.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> DiscountCouponList()
        {
            var values= await _discountService.GetAllCouponAsync();
            return Ok(values);  
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountCouponById(int id)
        {
            var values = await _discountService.GetByIdCouponAsync(id);
            return Ok(values);
        }


        [HttpPost]
        public async Task<IActionResult> CreateDiscountCoupon(CreateDiscountCouponDto createCouponDto)
        {
            var result = await _discountService.CreateCouponAsync(createCouponDto);
            return Ok(new { success = result });
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteDiscountCoupon(int id)
        {
            var values = _discountService.DeleteCouponAsync(id);
            return Ok("indirim kuponu başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscountCoupon(UpdateDiscountCouponDto updateCouponDto)
        {
            await _discountService.UpdateCouponAsync(updateCouponDto);

            var updatedCoupon = await _discountService.GetByIdCouponAsync(updateCouponDto.CouponId);
            if (updatedCoupon == null)
            {
                return NotFound("Kupon bulunamadı.");
            }

            return Ok("İndirim kuponu başarıyla güncellendi");
        }

    }
}
