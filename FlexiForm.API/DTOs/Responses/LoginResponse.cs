namespace FlexiForm.API.DTOs.Responses
{
    /// <summary>
    /// Represents the response returned after a successful user login.
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Gets or sets the JWT access token issued to the user.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the expiration date and time of the access token.
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Gets or sets the details of the authenticated user.
        /// </summary>
        public UserResponse User { get; set; }
    }
}
