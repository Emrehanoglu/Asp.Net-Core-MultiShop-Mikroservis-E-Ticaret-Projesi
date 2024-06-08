using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services;

public interface IDiscountService
{
    Task<List<ResultDiscountCouponDto>> GetAllCouponAsync(); 
    Task<GetByIdDiscountCouponDto> GetByIdCouponAsync(int id);
    Task CreateCouponAsync(CreateDiscountCouponDto createCouponDto);
    Task UpdateCouponAsync(UpdateDiscountCouponDto updateCouponDto);
    Task DeleteCouponAsync(int id);
    Task<ResultDiscountCouponDto> GetDiscountDetailByCode(string code);
}
