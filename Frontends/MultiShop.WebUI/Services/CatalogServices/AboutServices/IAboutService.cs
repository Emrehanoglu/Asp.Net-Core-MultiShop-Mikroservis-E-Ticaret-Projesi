using MultiShop.DtoLayer.CatalogDtos.AboutDtos;

namespace MultiShop.WebUI.Services.CatalogServices.AboutServices;

public interface IAboutService
{
    Task<List<ResultAboutDto>> GetAllAboutAsync();
    Task<UpdateAboutDto> GetByIdAboutAsync(string id);
    Task CreateAboutAsync(CreateAboutDto createAboutDto);
    Task UpdateAboutAsync(UpdateAboutDto updateAboutDto);
    Task DeleteAboutAsync(string id);
}
