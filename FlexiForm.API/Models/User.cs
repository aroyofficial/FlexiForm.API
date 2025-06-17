using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Models
{
    /// <summary>
    /// Represents a user in the system with personal and authentication-related details.
    /// </summary>
    public class User : BaseModel
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
        /// Gets or sets the password for the user's account.
        /// Note: This should be securely hashed before storage.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the gender of the user.
        /// </summary>
        public Gender Gender { get; set; }
    }
}
