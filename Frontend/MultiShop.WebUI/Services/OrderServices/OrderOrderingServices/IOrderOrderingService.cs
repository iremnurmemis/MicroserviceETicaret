using MultiShop.DtoLayer.OrderDtos.OrderingDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderingServices
{
    public interface IOrderOrderingService
    {
        Task<List<ResultOrderingByUserIdDto>> GetOrderingByUserId(string id);
    }
}
