﻿using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;
using System.Security.Claims;

namespace MultiShop.WebUI.Services.Concrete;

//public class IdentityService : IIdentityService
//{
//    private readonly HttpClient _httpClient;
//    private readonly IHttpContextAccessor _httpContextAccessor;
//    private readonly ClientSettings _clientSettings;
//    private readonly ServiceApiSettings _serviceApiSettings;

//    public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings)
//    {
//        _httpClient = httpClient;
//        _httpContextAccessor = httpContextAccessor;
//        _clientSettings = clientSettings.Value;
//        _serviceApiSettings = serviceApiSettings.Value;
//    }

//    public async Task<bool> GetRefreshToken()
//    {
//        //SignIn metodundaki gibi bir token üretimi gercekleştirilecek

//        var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
//        {
//            Address = _serviceApiSettings.IdentityServerUrl,
//            Policy = new DiscoveryPolicy
//            {
//                RequireHttps = false        //https yerine http kullanımı olacak
//            }
//        });

//        //token oluşturma işlemi...

//        var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

//        RefreshTokenRequest refreshTokenRequest = new()
//        {
//            ClientId = _clientSettings.MultiShopManagerClient.ClientId,
//            ClientSecret = _clientSettings.MultiShopManagerClient.ClientSecret,
//            RefreshToken = refreshToken,
//            Address = discoveryEndPoint.TokenEndpoint
//        };

//        var token = await _httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

//        var authenticationToken = new List<AuthenticationToken>()
//        {
//            new AuthenticationToken
//            {
//                Name = OpenIdConnectParameterNames.AccessToken,
//                Value = token.AccessToken
//            },

//            new AuthenticationToken
//            {
//                Name = OpenIdConnectParameterNames.RefreshToken,
//                Value = token.RefreshToken
//            },

//            new AuthenticationToken
//            {
//                Name = OpenIdConnectParameterNames.ExpiresIn,
//                Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString()
//            }
//        };

//        var result = await _httpContextAccessor.HttpContext.AuthenticateAsync();

//        var properties = result.Properties;
//        properties.StoreTokens(authenticationToken);

//        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.Principal,
//            properties);

//        return true;
//    }

//    public async Task<bool> SignIn(SignInDto signInDto)
//    {
//        //istek yapılacak parametrelerin konfigürasyonları yapıldı

//        var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
//        {
//            Address = _serviceApiSettings.IdentityServerUrl,
//            Policy = new DiscoveryPolicy
//            {
//                RequireHttps = false        //https yerine http kullanımı olacak
//            }
//        });

//        //şimdilik sadece manager kullanıcısı için calısıyorum
//        //manager kullanıcısı için parametreleri alıyorum...

//        var passwordTokenRequest = new PasswordTokenRequest
//        {
//            ClientId = _clientSettings.MultiShopManagerClient.ClientId,
//            ClientSecret = _clientSettings.MultiShopManagerClient.ClientSecret,
//            UserName = signInDto.Username,
//            Password = signInDto.Password,
//            Address = discoveryEndPoint.TokenEndpoint
//        };

//        //token oluşturma işlemi...

//        var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);

//        var userInfoRequest = new UserInfoRequest
//        {
//            Address = discoveryEndPoint.UserInfoEndpoint,
//            Token = token.AccessToken
//        };

//        //gelen token bilgisi içerisinden kullanıcı bilgilerini doldurdum

//        var userValues = await _httpClient.GetUserInfoAsync(userInfoRequest);

//        ClaimsIdentity claimsIdentity = new ClaimsIdentity(userValues.Claims,
//            CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");

//        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

//        var authenticationProperties = new AuthenticationProperties();
//        authenticationProperties.StoreTokens(new List<AuthenticationToken>()
//        {
//            new AuthenticationToken
//            {
//                Name = OpenIdConnectParameterNames.AccessToken,
//                Value = token.AccessToken
//            },

//            new AuthenticationToken
//            {
//                Name = OpenIdConnectParameterNames.RefreshToken,
//                Value = token.RefreshToken
//            },

//            new AuthenticationToken
//            {
//                Name = OpenIdConnectParameterNames.ExpiresIn,
//                Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString()
//            }
//        });

//        authenticationProperties.IsPersistent = false;

//        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
//            claimsPrincipal, authenticationProperties);

//        return true;
//    }
//}

public class IdentityService : IIdentityService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ClientSettings _clientSettings;
    private readonly ServiceApiSettings _serviceApiSettings;

    public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _clientSettings = clientSettings.Value;
        _serviceApiSettings = serviceApiSettings.Value;
    }

    public async Task<bool> GetRefreshToken()
    {
        var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _serviceApiSettings.IdentityServerUrl,
            Policy = new DiscoveryPolicy
            {
                RequireHttps = false
            }
        });

        var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

        RefreshTokenRequest refreshTokenRequest = new()
        {
            ClientId = _clientSettings.MultiShopManagerClient.ClientId,
            ClientSecret = _clientSettings.MultiShopManagerClient.ClientSecret,
            RefreshToken = refreshToken,
            Address = discoveryEndPoint.TokenEndpoint
        };

        var token = await _httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

        var authenticationToken = new List<AuthenticationToken>()
            {
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.AccessToken,
                    Value = token.AccessToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.RefreshToken,
                    Value = token.RefreshToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.ExpiresIn,
                    Value=DateTime.Now.AddSeconds(token.ExpiresIn).ToString()
                }
            };

        var result = await _httpContextAccessor.HttpContext.AuthenticateAsync();

        var properties = result.Properties;
        properties.StoreTokens(authenticationToken);

        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.Principal, properties);

        return true;
    }

    public async Task<bool> SignIn(SignInDto signInDto)
    {
        var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _serviceApiSettings.IdentityServerUrl,
            Policy = new DiscoveryPolicy
            {
                RequireHttps = false
            }
        });

        var passwordTokenRequest = new PasswordTokenRequest
        {
            ClientId = _clientSettings.MultiShopManagerClient.ClientId,
            ClientSecret = _clientSettings.MultiShopManagerClient.ClientSecret,
            UserName = signInDto.Username,
            Password = signInDto.Password,
            Address = discoveryEndPoint.TokenEndpoint
        };

        var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);

        var userInfoRequest = new UserInfoRequest
        {
            Token = token.AccessToken,
            Address = discoveryEndPoint.UserInfoEndpoint
        };

        var userValues = await _httpClient.GetUserInfoAsync(userInfoRequest);

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(userValues.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");

        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        var authenticationProperties = new AuthenticationProperties();

        authenticationProperties.StoreTokens(new List<AuthenticationToken>()
            {
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.AccessToken,
                    Value = token.AccessToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.RefreshToken,
                    Value = token.RefreshToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.ExpiresIn,
                    Value=DateTime.Now.AddSeconds(token.ExpiresIn).ToString()
                }
            });

        authenticationProperties.IsPersistent = false;

        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

        return true;
    }
}