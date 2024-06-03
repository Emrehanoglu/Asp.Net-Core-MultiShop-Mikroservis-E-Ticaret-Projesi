﻿using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;

namespace MultiShop.WebUI.Services.CatalogServices.CategoryServices;

public interface ICategoryService
{
    Task<List<ResultCategoryDto>> GetAllCategoryAsync();
    Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id);
    Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
    Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
    Task DeleteCategoryAsync(string id);
}
