using FlexiForm.API.DTOs.Requests;

namespace FlexiForm.API.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for authentication-related data operations.
    /// </summary>
    public interface    IAuthRepository
    {
        /// <summary>
        /// Adds a new OTP (One-Time Password) record to the data store.
        /// </summary>
        /// <param name="request">The OTP request data containing the value, generation time, expiration time, and creator information.</param>
        Task AddOTPAsync(OTPRequest request);
    }
}
