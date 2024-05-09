using MultiShop.Basket.Dtos;
using MultiShop.Basket.Settings;
using System.Text.Json;

namespace MultiShop.Basket.Services;

public class BasketService : IBasketService
{
    private readonly RedisService _redisService;

    public BasketService(RedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task DeleteBasket(string userId)
    {
        var status = await _redisService.GetDb().KeyDeleteAsync(userId);
    }

    public async Task<BasketTotalDto> GetBasket(string userId)
    {
        var existBasket = await _redisService.GetDb().StringGetAsync(userId);
        //Redis veritabanından okudugum JSON formatındaki veriyi Deserialize ederek return ediyorum.
        return JsonSerializer.Deserialize<BasketTotalDto>(existBasket);
    }

    public async Task SaveBasket(BasketTotalDto basketTotalDto)
    {
        //GetDb ile veritabanını getiriyorum
        await _redisService.GetDb().StringSetAsync(basketTotalDto.UserId, JsonSerializer.Serialize(basketTotalDto));
    }
}
