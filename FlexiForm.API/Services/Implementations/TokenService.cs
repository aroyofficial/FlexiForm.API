using FlexiForm.API.Configurations;
using FlexiForm.API.Internals;
using FlexiForm.API.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlexiForm.API.Services.Implementations
{
    /// <summary>
    /// Service implementation for generating JWT tokens based on provided token payload.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly TokenConfiguration _configuration;
        private readonly IHttpContextAccessor _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService"/> class.
        /// </summary>
        /// <param name="configuration">
        /// The token configuration options containing secret key, issuer, audience, and expiry settings.
        /// </param>
        public TokenService(IOptions<TokenConfiguration> configuration, IHttpContextAccessor context)
        {
            _configuration = configuration.Value;
            _context = context;
        }

        /// <inheritdoc/>
        public Token Generate(TokenPayload payload)
        {
            var key = Encoding.UTF8.GetBytes(_configuration.SecretKey);
            var issuer = _configuration.Issuer;
            var audience = _configuration.Audience;

            var token = new Token()
            {
                IssuedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_configuration.ExpiryMinutes)
            };

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, payload.Subject.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, payload.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("user_id", payload.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                          new DateTimeOffset(token.IssuedAt).ToUnixTimeSeconds().ToString(),
                          ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Exp,
                          new DateTimeOffset(token.ExpiresAt).ToUnixTimeSeconds().ToString(),
                          ClaimValueTypes.Integer64)
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            );

            var jwtToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: token.IssuedAt,
                expires: token.ExpiresAt,
                signingCredentials: signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            token.Value = tokenHandler.WriteToken(jwtToken);

            return token;
        }
    }
}
