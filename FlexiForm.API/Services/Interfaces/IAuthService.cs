using FlexiForm.API.DTOs.Requests;
using FlexiForm.API.DTOs.Responses;

namespace FlexiForm.API.Services.Interfaces
{
    /// <summary>
    /// Defines methods for handling user authentication operations.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates a user based on the provided login credentials.
        /// </summary>
        /// <param name="request">The login request containing email and password.</param>
        /// <returns>
        /// A <see cref="LoginResponse"/> containing the access token, expiry information,
        /// and authenticated user details if credentials are valid.
        /// </returns>
        Task<LoginResponse> LoginAsync(LoginRequest request);

        /// <summary>
        /// Generates a new OTP (One-Time Password) for the specified email address asynchronously.
        /// </summary>
        /// <param name="email">The email address for which the OTP is to be generated.</param>
        Task GenerateOTPAsync(string email);

    }
}
