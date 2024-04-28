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
        var value = _mapper.Map<Category>(createCategoryDto);
        await _categoryCollection.InsertOneAsync(value); //MongoDb 'de ekleme işlemi InsertOneAsync ile yapılır.
    }

    public async Task DeleteCategoryAsync(string id)
    {
        await _categoryCollection.DeleteOneAsync(x => x.CategoryId == id);
    }

    public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
    {
        var values = await _categoryCollection.Find(x=> true).ToListAsync();
        return _mapper.Map<List<ResultCategoryDto>>(values);
    }

    public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
    {
        var value = await _categoryCollection.Find<Category>(x => x.CategoryId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetByIdCategoryDto>(value);
    }

    public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
    {
        var values = _mapper.Map<Category>(updateCategoryDto);
        //FindOneAndReplaceAsync metody ile
        //updateCategoryDto 'dan gelen CategorydId ile tablodaki kaydı bul ve values ile değiştir demiş olurum.
        await _categoryCollection.FindOneAndReplaceAsync(x=>x.CategoryId==updateCategoryDto.CategoryId, values);
    }
}
