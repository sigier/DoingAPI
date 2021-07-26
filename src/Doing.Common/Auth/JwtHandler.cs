using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Doing.Common.Auth
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtOptions _options;

        private  JwtSecurityTokenHandler _securityTokenHandler =
                new JwtSecurityTokenHandler();

        private readonly SecurityKey _securityKey;

        private readonly SigningCredentials _signingCredentials;

        private readonly JwtHeader _jwtHeader;

        private readonly TokenValidationParameters _tokenValidationParams;

        public JwtHandler(IOptions<JwtOptions> options)
        {
            _options = options.Value;

            _securityKey =
                     new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            
            _signingCredentials =
                     new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);

            _jwtHeader = new JwtHeader(_signingCredentials);

            _tokenValidationParams = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuer = options.Value.IssuerOfToken,
                IssuerSigningKey = _securityKey
            };
        
        }



        public JsonWebToken Create(Guid userId)
        {
            var currentDate = DateTime.UtcNow;

            var expirationDate = currentDate.AddMinutes(_options.ExpirationinMinutes);

            var centuryStart = new DateTime(1970, 1, 1).ToUniversalTime();

            var longExpiration =
                (long)(new TimeSpan(expirationDate.Ticks - centuryStart.Ticks).TotalSeconds);
            
            var longNow = 
                (long)(new TimeSpan(currentDate.Ticks - centuryStart.Ticks).TotalSeconds);

            var payload = new JwtPayload
            {   
                { "sub", userId },
                { "iss", _options.IssuerOfToken },
                { "iat", longNow },
                { "exp", longExpiration },
                { "unique_name", userId }
            };

            var jwt = new JwtSecurityToken(_jwtHeader, payload);

            var token = _securityTokenHandler.WriteToken(jwt);

            return new JsonWebToken 
            {
                Token = token,
                Expires = longExpiration
            };
            

        }
    }
}