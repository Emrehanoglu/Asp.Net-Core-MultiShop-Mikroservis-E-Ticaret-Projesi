using MultiShop.Catalog.Dtos.CategoryDtos;

namespace MultiShop.Catalog.Services.CatalogServices;

public interface ICategoryService
{
    Task<List<ResultCategoryDto>> GetAllCategoryAsync();
    Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id);
    Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
    Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
    Task DeleteCategoryAsync(string id);
}
