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
        /// Generates a One-Time Password (OTP) for the specified user and sends it to their email address for password reset purposes.
        /// </summary>
        /// <param name="request">The request object containing the email address of the user requesting the OTP.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task GenerateOTPAsync(ForgotPasswordRequest request);

        /// <summary>
        /// Resets the user's password using the provided OTP and new password.
        /// </summary>
        /// <param name="request">
        /// The request payload containing the user's email address, new password, and one-time password (OTP).
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation of resetting the password.
        /// </returns>
        Task ResetPasswordAsync(ResetPasswordRequest request);
    }
}
