using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;

namespace MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;

public interface IOfferDiscountService
{
    Task<List<ResultOfferDiscountDto>> GetAllOfferDiscountAsync();
    Task<UpdateOfferDiscountDto> GetByIdOfferDiscountAsync(string id);
    Task CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto);
    Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto);
    Task DeleteOfferDiscountAsync(string id);
}
