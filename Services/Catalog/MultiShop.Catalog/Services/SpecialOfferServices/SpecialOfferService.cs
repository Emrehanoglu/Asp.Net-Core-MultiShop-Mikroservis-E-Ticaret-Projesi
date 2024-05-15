using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.SpecialOfferServices;

public class SpecialOfferService: ISpecialOfferService
{
    private readonly IMongoCollection<SpecialOffer> _SpecialOfferCollection;
    private readonly IMapper _mapper;

    public SpecialOfferService(IDatabaseSettings _databaseSettings, IMapper mapper)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString); //appsettings.json içerisinde tanımladığım connectionString 'i aldım.
        var database = client.GetDatabase(_databaseSettings.DatabaseName); //bu noktada artık veritabanına gitmiş oldum.
        _SpecialOfferCollection = database.GetCollection<SpecialOffer>(_databaseSettings.SpecialOfferCollectionName); //appsettings.json içerisinde tanımladığım tablo isimlerini veritabanına yansttım.
        _mapper = mapper;
    }

    public async Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto)
    {
        var value = _mapper.Map<SpecialOffer>(createSpecialOfferDto);
        await _SpecialOfferCollection.InsertOneAsync(value); //MongoDb 'de ekleme işlemi InsertOneAsync ile yapılır.
    }

    public async Task DeleteSpecialOfferAsync(string id)
    {
        await _SpecialOfferCollection.DeleteOneAsync(x => x.SpecialOfferId == id);
    }

    public async Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync()
    {
        var values = await _SpecialOfferCollection.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultSpecialOfferDto>>(values);
    }

    public async Task<GetByIdSpecialOfferDto> GetByIdSpecialOfferAsync(string id)
    {
        var value = await _SpecialOfferCollection.Find<SpecialOffer>(x => x.SpecialOfferId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetByIdSpecialOfferDto>(value);
    }

    public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto)
    {
        var values = _mapper.Map<SpecialOffer>(updateSpecialOfferDto);
        //FindOneAndReplaceAsync metody ile
        //updateSpecialOfferDto 'dan gelen SpecialOfferdId ile tablodaki kaydı bul ve values ile değiştir demiş olurum.
        await _SpecialOfferCollection.FindOneAndReplaceAsync(x => x.SpecialOfferId == updateSpecialOfferDto.SpecialOfferId, values);
    }
}
