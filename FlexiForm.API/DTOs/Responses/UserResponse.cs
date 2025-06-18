using FlexiForm.API.Enumerations;

namespace FlexiForm.API.DTOs.Responses
{
    /// <summary>
    /// Represents the response data returned for a user.
    /// </summary>
    public class UserResponse : BaseDTO
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the gender of the user.
        /// </summary>
        public Gender Gender { get; set; }
    }
}
