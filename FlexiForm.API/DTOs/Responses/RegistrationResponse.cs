using FlexiForm.API.Enumerations;

namespace FlexiForm.API.DTOs.Responses
{
    /// <summary>
    /// Represents the response returned after a user has been successfully registered.
    /// </summary>
    public class RegistrationResponse : BaseDTO
    {
        /// <summary>
        /// Gets or sets the first name of the registered user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the registered user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the registered user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the gender of the registered user.
        /// </summary>
        public Gender Gender { get; set; }
    }
}
