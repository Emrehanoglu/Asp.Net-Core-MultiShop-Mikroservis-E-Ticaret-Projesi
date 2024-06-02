using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MultiShop.WebUI.Services.Interfaces;
using System.Net.Http.Headers;
using System.Net;

namespace MultiShop.WebUI.Handlers;

public class ResourceOwnerPasswordTokenHandler:DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IIdentityService _identityService;

    public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor contextAccessor, IIdentityService identityService)
    {
        _httpContextAccessor = contextAccessor;
        _identityService = identityService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        //giriş yapan kullanıcının token bilgisini alıyorum

        var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await base.SendAsync(request, cancellationToken);

        if(response.StatusCode == HttpStatusCode.Unauthorized)
        {
            //kullanıcı var olan token ile sisteme giremezse yeni bir token üret
            var tokenResponse = await _identityService.GetRefreshToken();

            if(tokenResponse != null)
            {
                //tekrar erişmeyi dene
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                response = await base.SendAsync(request, cancellationToken);
            }
        }

        if(response.StatusCode == HttpStatusCode.Unauthorized)
        {
            //yine giriş işlemi olmadıysa hata mesajı dön
        }

        return response;
    }
}
