using IdentityServer4;
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
            },

            new ApiResource("ResourceDiscount")
            {
                //token içerisinde ResourceDiscount key 'ine sahip bir kullanıcı asagıdaki yetkilere sahip olacak
                Scopes={ "DiscountFullPermission" }
            },

            new ApiResource("ResourceOrder")
            {
                //token içerisinde ResourceOrder key 'ine sahip bir kullanıcı asagıdaki yetkilere sahip olacak
                Scopes={ "OrderFullPermission" }
            },

            new ApiResource("ResourceCargo")
            {
                //token içerisinde ResourceCargo key 'ine sahip bir kullanıcı asagıdaki yetkilere sahip olacak
                Scopes={ "CargoFullPermission" }
            },

            new ApiResource("ResourceBasket")
            {
                //token içerisinde ResourceBasket key 'ine sahip bir kullanıcı asagıdaki yetkilere sahip olacak
                Scopes={ "BasketFullPermission" }
            },

            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
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
            new ApiScope("CatalogReadPermission","Reading authority for catalog operations"),

            new ApiScope("DiscountFullPermission","Full authority for discount operations"),

            new ApiScope("OrderFullPermission","Full authority for order operations"),

            new ApiScope("CargoFullPermission","Full authority for cargo operations"),

            new ApiScope("BasketFullPermission","Full authority for basket operations"),

            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<Client> Clients => new Client[]
        {
            //Visitor rolundeki kullanıcının sahip olacagı izinler
            new Client
            {
                ClientId = "MultiShopVisitorId",
                ClientName = "Multi Shop Visitor User",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("multishopsecret".Sha256())},
                AllowedScopes = { "CatalogFullPermission", "CatalogReadPermission", "BasketFullPermission" } //kullanıcının hangi yetkilere sahip olacagını burada belirliyorum
            },

            //Manager rolundeki kullanıcının sahip olacagı izinler
            new Client
            {
                ClientId = "MultiShopManagerId",
                ClientName = "Multi Shop Manager User",
                //AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = {new Secret("multishopsecret".Sha256())},
                AllowedScopes = { "CatalogFullPermission","CatalogReadPermission", "BasketFullPermission" } //kullanıcının hangi yetkilere sahip olacagını burada belirliyorum
            },

            //Admin rolundeki kullanıcının sahip olacagı izinler
            new Client
            {
                ClientId = "MultiShopAdminId",
                ClientName = "Multi Shop Admin User",
                //AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = {new Secret("multishopsecret".Sha256())},
                AllowedScopes = { "CatalogFullPermission","CatalogReadPermission",
                "DiscountFullPermission", "OrderFullPermission", "CargoFullPermission",
                "BasketFullPermission",
                IdentityServerConstants.LocalApi.ScopeName,
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.Profile}, //kullanıcının hangi yetkilere sahip olacagını burada belirliyorum
                AccessTokenLifetime=600 //token 'ın ömrü 600 saniye yanı 10dk olacak.
            },
        };
    }
}