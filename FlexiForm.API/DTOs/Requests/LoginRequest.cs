namespace FlexiForm.API.DTOs.Requests
{
    /// <summary>
    /// Represents the login request data sent by the user.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Gets or sets the email address of the user attempting to log in.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user attempting to log in.
        /// </summary>
        public string Password { get; set; }
    }
}
