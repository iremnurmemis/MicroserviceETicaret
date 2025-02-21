using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoOperationDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoOperationsController : ControllerBase
    {
        private readonly ICargoOperationService _cargoOperationService;

        public CargoOperationsController(ICargoOperationService cargoOperationService)
        {
            _cargoOperationService = cargoOperationService;
        }

        [HttpGet]
        public IActionResult CargoOperationList()
        {
            var values = _cargoOperationService.TGetAll();
            return Ok(values);
          
        }


        [HttpGet("{id}")]
        public IActionResult GetCargoOperationById(int id)
        {
            var value = _cargoOperationService.TGetById(id);
            return Ok(value);

        }

        [HttpDelete]
        public IActionResult RemoveCargoOperation(int id)
        {
            _cargoOperationService.TDelete(id);
            return Ok("kargo operasyonu başarıyla silindi");
        }

        [HttpPost]
        public IActionResult CreateCargoOperation(CreateCargoOperationDto cargoOperationDto)
        {
            _cargoOperationService.TInsert(new CargoOperation
            {
                Barcode = cargoOperationDto.Barcode,
                Description = cargoOperationDto.Description,
                OperationDate = cargoOperationDto.OperationDate,
            });

            return Ok("kargo operasyonu başarıyla oluşturuldu");
        }


        [HttpPut]
        public IActionResult UpdateCargoOperation(UpdateCargoOperationDto cargoOperationDto)
        {
            _cargoOperationService.TUpdate(new CargoOperation
            {
                CargoOperationId=cargoOperationDto.CargoOperationId,
                Barcode = cargoOperationDto.Barcode,
                Description = cargoOperationDto.Description,
                OperationDate = cargoOperationDto.OperationDate,
            });

            return Ok("kargo operasyonu başarıyla güncellendi");
        }
    }
}
