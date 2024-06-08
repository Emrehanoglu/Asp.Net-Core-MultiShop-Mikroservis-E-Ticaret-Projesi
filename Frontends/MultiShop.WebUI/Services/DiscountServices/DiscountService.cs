using MultiShop.DtoLayer.DiscountDtos;

namespace MultiShop.WebUI.Services.DiscountServices;

public class DiscountService : IDiscountService
{
    private readonly HttpClient _httpClient;
    public DiscountService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<GetDiscountCodeDetailByCodeDto> GetDiscountCode(string code)
    {
        var responseMessage = await _httpClient.GetAsync("discounts/GetDiscountDetailByCode/" + code);
        var values = await responseMessage.Content.ReadFromJsonAsync<GetDiscountCodeDetailByCodeDto>();
        return values;
    }
}
