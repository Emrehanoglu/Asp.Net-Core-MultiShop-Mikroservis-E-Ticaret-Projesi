using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly IMongoCollection<Contact> _contactCollection;
        private readonly IMapper _mapper;

        public ContactService(IDatabaseSettings _databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); //appsettings.json içerisinde tanımladığım connectionString 'i aldım.
            var database = client.GetDatabase(_databaseSettings.DatabaseName); //bu noktada artık veritabanına gitmiş oldum.
            _contactCollection = database.GetCollection<Contact>(_databaseSettings.ContactCollectionName); //appsettings.json içerisinde tanımladığım tablo isimlerini veritabanına yansttım.
            _mapper = mapper;
        }

        public async Task CreateContactAsync(CreateContactDto createContactDto)
        {
            var value = _mapper.Map<Contact>(createContactDto);
            await _contactCollection.InsertOneAsync(value); //MongoDb 'de ekleme işlemi InsertOneAsync ile yapılır.
        }

        public async Task DeleteContactAsync(string id)
        {
            await _contactCollection.DeleteOneAsync(x => x.ContactId == id);
        }

        public async Task<List<ResultContactDto>> GetAllContactAsync()
        {
            var values = await _contactCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultContactDto>>(values);
        }

        public async Task<GetByIdContactDto> GetByIdContactAsync(string id)
        {
            var value = await _contactCollection.Find<Contact>(x => x.ContactId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdContactDto>(value);
        }

        public async Task UpdateContactAsync(UpdateContactDto updateContactDto)
        {
            var values = _mapper.Map<Contact>(updateContactDto);
            //FindOneAndReplaceAsync metody ile
            //updateContactDto 'dan gelen ContactdId ile tablodaki kaydı bul ve values ile değiştir demiş olurum.
            await _contactCollection.FindOneAndReplaceAsync(x => x.ContactId == updateContactDto.ContactId, values);
        }
    }
}
