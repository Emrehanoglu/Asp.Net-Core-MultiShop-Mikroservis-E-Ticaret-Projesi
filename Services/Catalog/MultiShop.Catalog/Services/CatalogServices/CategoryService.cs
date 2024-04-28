using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.CatalogServices;

public class CategoryService : ICategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryService(IDatabaseSettings _databaseSettings, IMapper mapper)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString); //appsettings.json içerisinde tanımladığım connectionString 'i aldım.
        var database = client.GetDatabase(_databaseSettings.DatabaseName); //bu noktada artık veritabanına gitmiş oldum.
        _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName); //appsettings.json içerisinde tanımladığım tablo isimlerini veritabanına yansttım.
        _mapper = mapper;
    }

    public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        //var value = _mapper.Map<Category>(createCategoryDto);
        //await 
    }

    public Task DeleteCategoryAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ResultCategoryDto>> GetAllCategoryAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
    {
        throw new NotImplementedException();
    }
}
