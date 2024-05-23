namespace MultiShop.IdentityServer.Tools;

public class GetCheckAppUserViewModel
{
    //token 'ın içerisinde bulunacak bilgiler

    public string Id { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
    public bool IsExist { get; set; }
}
