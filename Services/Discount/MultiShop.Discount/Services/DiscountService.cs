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

    public async Task CreateCouponAsync(CreateDiscountCouponDto createCouponDto)
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

    public async Task DeleteCouponAsync(int id)
    {
        string query = "Delete from Coupons where CouponId=@couponId";

        var parameters = new DynamicParameters();
        parameters.Add("@couponId", id);

        using(var connection = _dapperContext.CreateConnection())
        {
            await connection.ExecuteAsync(query,parameters);
        }
    }

    public async Task<List<ResultDiscountCouponDto>> GetAllCouponAsync()
    {
        string query = "select * from Coupons";

        using (var connection = _dapperContext.CreateConnection())
        {
            var values = await connection.QueryAsync<ResultDiscountCouponDto>(query);
            return values.ToList();
        }
    }

    public async Task<GetByIdDiscountCouponDto> GetByIdCouponAsync(int id)
    {
        string query = "select * from Coupons where CouponId=@couponId";

        var parameters = new DynamicParameters();
        parameters.Add("@couponId", id);

        using (var connection = _dapperContext.CreateConnection())
        {
            var values = await connection.QueryFirstOrDefaultAsync<GetByIdDiscountCouponDto>(query, parameters);
            return values;
        }
    }

    public async Task<ResultDiscountCouponDto> GetDiscountDetailByCode(string code)
    {
        string query = "select * from Coupons where Code = @code";

        var parameters = new DynamicParameters();
        parameters.Add("@code", code);

        using (var connection = _dapperContext.CreateConnection())
        {
            var values = await connection.QueryFirstOrDefaultAsync<ResultDiscountCouponDto>(query, parameters);
            return values;
        }
    }

    public async Task UpdateCouponAsync(UpdateDiscountCouponDto updateCouponDto)
    {
        string query = "update Coupons set Code=@code, Rate=@rate, IsActive=@isActive, ValidDate=@validDate" +
            "where CouponId=@couponId";

        var parameters = new DynamicParameters();
        parameters.Add("@couponId", updateCouponDto.CouponId);
        parameters.Add("@code", updateCouponDto.Code);
        parameters.Add("@rate", updateCouponDto.Rate);
        parameters.Add("@isActive", updateCouponDto.IsActive);
        parameters.Add("@validDate", updateCouponDto.ValidDate);

        using (var connection = _dapperContext.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
