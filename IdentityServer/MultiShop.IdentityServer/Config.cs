using IdentityServer4.Models;
using System.Collections.Generic;

namespace MultiShop.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            //ApiResources her oluşturuldugunda, her bir mikroservisim için asagıdaki yetkilendirme sartları olusacak

            new ApiResource("ResourceCatalog")
            {
                //token içerisinde ResourceCatalog key 'ine sahip bir kullanıcı asagıdaki yetkilere sahip olacak
                Scopes={"CatalogFullPermission","CatalogReadPermission"}
            }
        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            //token içerisinde IdentityResource key 'ine sahip bir kullanıcının token 'ı içerisinde bulunan
            //asagıdaki bilgilerine erisebileceğimi belirttim.
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            //token bilgisi içerisinde CatalogFullPermission yetkisi var ise parantez içerisindeki ikinci çifttırnak
            //içerisinde bulunan ifadedeki işlemleri yapabilsin. Yani burada CatalogFullPermission sahip bir kullanıcı
            //Catalog işlemleri için full yetkiye sahip oluyor.
            new ApiScope("CatalogFullPermission","Full authority for catalog operations"),

            //token bilgisi içerisinde CatalogReadPermission yetkisi var ise parantez içerisindeki ikinci çifttırnak
            //içerisinde bulunan ifadedeki işlemleri yapabilsin. Yani burada CatalogReadPermission sahip bir kullanıcı
            //Catalog işlemleri için sadece okuma yetkisine sahip oluyor.
            new ApiScope("CatalogReadPermission","Reading authority for catalog operations")
        };
    }
}