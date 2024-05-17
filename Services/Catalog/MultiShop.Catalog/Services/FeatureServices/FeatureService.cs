using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.FeatureDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.FeatureServices;

public class FeatureService:IFeatureService
{
    private readonly IMongoCollection<Feature> _featureCollection;
    private readonly IMapper _mapper;

    public FeatureService(IDatabaseSettings _databaseSettings, IMapper mapper)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString); //appsettings.json içerisinde tanımladığım connectionString 'i aldım.
        var database = client.GetDatabase(_databaseSettings.DatabaseName); //bu noktada artık veritabanına gitmiş oldum.
        _featureCollection = database.GetCollection<Feature>(_databaseSettings.FeatureCollectionName); //appsettings.json içerisinde tanımladığım tablo isimlerini veritabanına yansttım.
        _mapper = mapper;
    }

    public async Task CreateFeatureAsync(CreateFeatureDto createFeatureDto)
    {
        var value = _mapper.Map<Feature>(createFeatureDto);
        await _featureCollection.InsertOneAsync(value); //MongoDb 'de ekleme işlemi InsertOneAsync ile yapılır.
    }

    public async Task DeleteFeatureAsync(string id)
    {
        await _featureCollection.DeleteOneAsync(x => x.FeatureId == id);
    }

    public async Task<List<ResultFeatureDto>> GetAllFeatureAsync()
    {
        var values = await _featureCollection.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultFeatureDto>>(values);
    }

    public async Task<GetByIdFeatureDto> GetByIdFeatureAsync(string id)
    {
        var value = await _featureCollection.Find<Feature>(x => x.FeatureId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetByIdFeatureDto>(value);
    }

    public async Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto)
    {
        var values = _mapper.Map<Feature>(updateFeatureDto);
        //FindOneAndReplaceAsync metody ile
        //updateFeatureDto 'dan gelen FeaturedId ile tablodaki kaydı bul ve values ile değiştir demiş olurum.
        await _featureCollection.FindOneAndReplaceAsync(x => x.FeatureId == updateFeatureDto.FeatureId, values);
    }
}
