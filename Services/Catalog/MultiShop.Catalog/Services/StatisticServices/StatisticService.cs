using MongoDB.Driver;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.StatisticServices;

public class StatisticService : IStatisticService
{
    private readonly IMongoCollection<Product> _productCollection;
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMongoCollection<Brand> _brandCollection;

    public StatisticService(IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
        _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
        _brandCollection = database.GetCollection<Brand>(_databaseSettings.BrandCollectionName);
    }

    public async Task<long> GetBrandCount()
    {
        return await _brandCollection.CountDocumentsAsync(FilterDefinition<Brand>.Empty);
    }

    public Task<long> GetCategoryCount()
    {
        throw new NotImplementedException();
    }

    public Task<string> GetMaxPriceProductName()
    {
        throw new NotImplementedException();
    }

    public Task<string> GetMinPriceProductName()
    {
        throw new NotImplementedException();
    }

    public Task<decimal> GetProductAvgPrice()
    {
        throw new NotImplementedException();
    }

    public Task<long> GetProductCount()
    {
        throw new NotImplementedException();
    }
}
