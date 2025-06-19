using FlexiForm.API.Internals;

namespace FlexiForm.API.Services.Interfaces
{
    /// <summary>
    /// Defines methods for generating tokens used in authentication and authorization.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates a new token based on the provided token payload.
        /// </summary>
        /// <param name="payload">The payload containing claims and metadata for the token.</param>
        /// <returns>
        /// A <see cref="Token"/> instance containing the signed token string and its expiration details.
        /// </returns>
        Token Generate(TokenPayload payload);
    }
}
