﻿using IdentityModel.Client;
using Microsoft.Extensions.Options;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Services.Concrete;

public class IdentityService : IIdentityService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ClientSettings _clientSettings;

    public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _clientSettings = clientSettings.Value;
    }
    public async Task<bool> SignIn(SignUpDto signUpDto)
    {
        //istek yapılacak parametrelerin konfigürasyonları yapıldı

        var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = "http://localhost:5001",
            Policy = new DiscoveryPolicy
            {
                RequireHttps = false        //https yerine http kullanımı olacak
            }
        });

        //şimdilik sadece manager kullanıcısı için calısıyorum

        var passwordTokenRequest = new PasswordTokenRequest
        {
            ClientId = _clientSettings.MultiShopManagerId.ClientId,
            ClientSecret = _clientSettings.MultiShopManagerId.ClientSecret,
            UserName = signUpDto.Username,
            Password = signUpDto.Password,
            Address = discoveryEndPoint.TokenEndpoint
        };
    }
}
