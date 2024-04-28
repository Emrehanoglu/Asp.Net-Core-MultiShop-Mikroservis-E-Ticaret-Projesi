using MultiShop.Catalog.Dtos.ProductDetailDtos;

namespace MultiShop.Catalog.Services.ProductDetailServices;

public interface IProductDetailService
{
    Task<List<ResultProductDetailDto>> GetAllProductDetailAsync();
    Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id);
    Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto);
    Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto);
    Task DeleteProductDetailAsync(string id);
}
