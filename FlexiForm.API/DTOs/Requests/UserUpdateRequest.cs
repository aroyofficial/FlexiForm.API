using FlexiForm.API.Enumerations;

namespace FlexiForm.API.DTOs.Requests
{
    /// <summary>
    /// Represents the request payload for updating a user's profile information.
    /// </summary>
    public class UserUpdateRequest
    {
        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the user's gender.
        /// </summary>
        public Gender? Gender { get; set; }
    }
}
