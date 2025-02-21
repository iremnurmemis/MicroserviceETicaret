using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoDetailsController : ControllerBase
    {
        private readonly ICargoDetailService _cargoDetailService;

        public CargoDetailsController(ICargoDetailService cargoDetailService)
        {
            _cargoDetailService = cargoDetailService;
        }

        [HttpGet]
        public IActionResult CargoDetailList()
        {
            var values=_cargoDetailService.TGetAll();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetCargoDetailById(int id)
        {
            var value = _cargoDetailService.TGetById(id);
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateCargoDetail(CreateCargoDetailDto cargoDetailDto)
        {
            _cargoDetailService.TInsert(new CargoDetail
            {
                Barcode = cargoDetailDto.Barcode,
                CargoCompanyId = cargoDetailDto.CargoCompanyId,
                ReceiverCustomer = cargoDetailDto.ReceiverCustomer,
                SenderCustomer = cargoDetailDto.SenderCustomer,
            });

            return Ok("kargo detayları başarıyla eklendi");
        }

        [HttpPut]
        public IActionResult UpdateCargoDetail(UpdateCargoDetailDto cargoDetailDto)
        {
            _cargoDetailService.TUpdate(new CargoDetail
            {
                CargoDetailId=cargoDetailDto.CargoDetailId,
                Barcode = cargoDetailDto.Barcode,
                CargoCompanyId = cargoDetailDto.CargoCompanyId,
                ReceiverCustomer = cargoDetailDto.ReceiverCustomer,
                SenderCustomer = cargoDetailDto.SenderCustomer,
            });

            return Ok("kargo detayları başarıyla güncellendi");
        }

        [HttpDelete]
        public IActionResult RemoveCargoDetail(int id)
        {
            _cargoDetailService.TDelete(id);
            return Ok("kargo detayları silindi");
        }
    }
}
