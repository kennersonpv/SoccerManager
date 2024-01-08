using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SoccerManager.Api.Shared.Entities;
using SoccerManager.Api.Shared.OptionsSetup;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SoccerManager.Api.Shared.Authentication
{
    internal sealed class JwtProvider : IJwtProvider
    {
        private const int EXPIRATION_HOURS = 8;
        private readonly JwtOptions _jwtOptions;


        public JwtProvider(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GetJwtToken(string email)
        {
            var claims = new Claim[] {
                new Claim(JwtRegisteredClaimNames.Email, email)
            };

            var singinCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                null,
                DateTime.Now.AddHours(EXPIRATION_HOURS),
                singinCredentials);

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
