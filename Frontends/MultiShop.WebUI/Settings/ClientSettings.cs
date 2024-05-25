namespace MultiShop.WebUI.Settings;

public class ClientSettings
{
    public Client MultiShopVisitorId { get; set; }
    public Client MultiShopManagerId { get; set; }
    public Client MultiShopAdminId { get; set; }
}

public class Client
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}
