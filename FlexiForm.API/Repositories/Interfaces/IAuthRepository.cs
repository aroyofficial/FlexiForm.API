using FlexiForm.API.DTOs.Requests;
using FlexiForm.API.Internals;
using FlexiForm.API.Models;

namespace FlexiForm.API.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for authentication-related data operations.
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// Adds a new OTP (One-Time Password) record to the data store.
        /// </summary>
        /// <param name="request">The OTP request data containing the value, generation time, expiration time, and creator information.</param>
        Task AddOTPAsync(OTPRequest request);

        /// <summary>
        /// Retrieves the one-time password (OTP) associated with the specified user ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose OTP is to be retrieved.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the <see cref="OTP"/> entity linked to the specified user ID.
        /// </returns>
        Task<OTP> GetOTPAsync(Guid userId);

        /// <summary>
        /// Resets the password for the specified user using the details provided in the reset password request.
        /// </summary>
        /// <param name="userId">
        /// The unique identifier of the user whose password is to be reset.
        /// </param>
        /// <param name="request">
        /// The reset password request containing the new password and OTP for validation.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation of resetting the user's password.
        /// </returns>
        Task ResetPasswordAsync(Guid userId, ResetPasswordRequest request);
    }
}
