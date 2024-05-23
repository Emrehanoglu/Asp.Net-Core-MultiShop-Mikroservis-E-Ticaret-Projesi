using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MultiShop.IdentityServer.Tools;

public class JwtTokenGenerator
{
    public static TokenResponseViewModel GenerateToken(GetCheckAppUserViewModel model)
    {
        //burada token üretilecek

        var claims = new List<Claim>();

        //Role kontrolü yapıyorum

        if (!string.IsNullOrWhiteSpace(model.Role))
        {
            claims.Add(new Claim(ClaimTypes.Role, model.Role));
        }
        else
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, model.Id));
        }

        //Username kontrolü yapıyorum

        if (!string.IsNullOrWhiteSpace(model.Username))
        {
            claims.Add(new Claim("Username", model.Username));
        }

        //Bu noktadan itibaren artık token oluşmaya baslıyor

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));
        var signInCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire);
        JwtSecurityToken token = new JwtSecurityToken(issuer:JwtTokenDefaults.ValidIssuer,
            audience:JwtTokenDefaults.ValidAudience, claims: claims, notBefore: DateTime.UtcNow,
            expires: expireDate, signingCredentials:signInCredential);

        //artık üretilen token 'ı geri dönebilirim.

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        return new TokenResponseViewModel(tokenHandler.WriteToken(token), expireDate);
    }
}
