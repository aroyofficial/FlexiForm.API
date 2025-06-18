using FlexiForm.API.Internals;
using FlexiForm.API.Models;

namespace FlexiForm.API.Repositories.Interfaces
{
    /// <summary>
    /// Defines operations related to user data access and management.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Creates a new user record asynchronously in the database.
        /// </summary>
        /// <param name="user">The <see cref="User"/> object containing user details to be created.</param>
        /// <returns>
        /// A task that represents the asynchronous operation and returns the identifier of the newly created user.
        /// </returns>
        Task<int> CreateAsync(User user);

        /// <summary>
        /// Asynchronously retrieves a user based on the provided lookup criteria.
        /// </summary>
        /// <param name="request">
        /// The <see cref="UserLookupRequest"/> object containing either the user's ID or email.
        /// At least one of the fields must be provided to perform the lookup.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation and returns the matching <see cref="User"/> object,
        /// or <c>null</c> if no user is found.
        /// </returns>
        Task<User> GetAsync(UserLookupRequest request);
    }
}
