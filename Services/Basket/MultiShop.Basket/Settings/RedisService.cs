using StackExchange.Redis;

namespace MultiShop.Basket.Settings;

public class RedisService
{
    public string _host { get; set; }
    public int _port { get; set; }
    private ConnectionMultiplexer _connectionMultiplexer;

    public RedisService(string host, int port)
    {
        _host = host;
        _port = port;
    }
    //Discount mikroservisinde DapperContext sınıfımın içerisindeki CreateConnection gibi,
    //Connect metodu cagırıldığında veritabanına baglanmıs olucam.
    public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

    //Redis ilk kuruldugunda 16 adet database ile kurulmus oluyor,
    //Bunlardan 0. database yani ilk sıradaki database 'i kullanacagım demiş oldum.
    public IDatabase GetDb(int db = 1) => _connectionMultiplexer.GetDatabase(0);
}
