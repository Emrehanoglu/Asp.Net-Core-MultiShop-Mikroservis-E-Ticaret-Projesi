using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services;

public class DiscountService : IDiscountService
{
    private readonly DapperContext _dapperContext;

    public DiscountService(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task CreateCouponAsync(CreateCouponDto createCouponDto)
    {
        string query = "insert into Coupons (Code,Rate,IsActive,ValidDate) values" +
            "(@code,@rate,@isActive,@validDate)";
        
        var parameters = new DynamicParameters();
        parameters.Add("@code",createCouponDto.Code);
        parameters.Add("@rate", createCouponDto.Rate);
        parameters.Add("@isActive", createCouponDto.IsActive);
        parameters.Add("@validDate", createCouponDto.ValidDate);

        using (var connection = _dapperContext.CreateConnection())
        {
            //baglantımı acıyorum, önce DapperContext içerisindeki DefaultConnection 'a git diyorum,
            //DefaultConnection 'da appsettings.json içerisindeki ConnectionStrings 'imi tutuyor.
            //daha sonra yukarıdaki sorguyu, parametreler ile birlikte ilgili veritabanında çalıştır demiş oldum.
            await connection.ExecuteAsync(query,parameters);
        };
    }

    public Task DeleteCouponAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ResultCouponDto>> GetAllCouponAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GetByIdCouponDto> GetByIdCouponAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCouponAsync(UpdateCouponDto updateCouponDto)
    {
        throw new NotImplementedException();
    }
}
