using FlexiForm.API.DTOs.Requests;
using FlexiForm.API.DTOs.Responses;

namespace FlexiForm.API.Services.Interfaces
{
    /// <summary>
    /// Defines service-level operations related to user management.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Asynchronously registers a new user based on the provided registration request data.
        /// </summary>
        /// <param name="request">The <see cref="RegistrationRequest"/> object containing user registration details.</param>
        /// <returns>
        /// A task that represents the asynchronous operation and returns a <see cref="RegistrationResponse"/>
        /// indicating the result of the registration process.
        /// </returns>
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);

        /// <summary>
        /// Asynchronously retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to retrieve.</param>
        /// <returns>
        /// A task that represents the asynchronous operation and returns a <see cref="UserResponse"/> 
        /// containing the user's data if found; otherwise, <c>null</c>.
        /// </returns>
        Task<UserResponse> GetAsync(int id);

        /// <summary>
        /// Updates the profile information of a user with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to update.</param>
        /// <param name="request">An object containing the updated user details.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains a <see cref="UserResponse"/> object with the updated user information.
        /// </returns>
        Task<UserResponse> UpdateAsync(int id, UserUpdateRequest request);
    }
}
