using MultiShop.DtoLayer.OrderDtos.OrderAddressDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderAddressServices
{
    public interface IOrderAddressService
    {
        //Task<List<ResultOrderAddressDto>> GetAllOrderAddressAsync();
        //Task<UpdateOrderAddressDto> GetByIdOrderAddressAsync(string id);
        Task CreateOrderAddressAsync(CreateOrderAddressDto createOrderAddressDto);
        //Task UpdateOrderAddressAsync(UpdateOrderAddressDto updateOrderAddressDto);
        //Task DeleteOrderAddressAsync(string id);
    }
}
