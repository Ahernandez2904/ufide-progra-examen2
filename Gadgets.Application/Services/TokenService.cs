using Gadgets.Application.Configurations;
using Gadgets.Application.Contracts.Authentication;
using Gadgets.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Gadgets.Application.Services
{
    public class TokenService : ITokenService
    {
        public TokenService(IOptions<JwtConfiguration> configuration)
        {
            Configuration = configuration.Value;
        }

        JwtConfiguration Configuration;

        public string Authenticate(User user)
        {
            var claims =
                new[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
                };

            var securityKey = 
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.Key));
            var credentials =
                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var descriptor =
                new JwtSecurityToken
                (
                    Configuration.Issuer,
                    Configuration.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(Configuration.Timeout),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(descriptor);
        }

        public bool Validate(string token)
        {
            var securityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.Key));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken
                    (
                        token,
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration.Issuer,
                            ValidAudience = Configuration.Audience,
                            IssuerSigningKey = securityKey
                        },
                        out SecurityToken validatedToken
                    );
            }
            catch
            { return false; }

            return true;
        }
    }
}
