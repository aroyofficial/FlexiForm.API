namespace FlexiForm.API.DTOs.Requests
{
    /// <summary>
    /// Represents the request payload for initiating a forgot password operation.
    /// </summary>
    public class ForgotPasswordRequest
    {
        /// <summary>
        /// Gets or sets the email address associated with the user account for which the password reset is requested.
        /// </summary>
        public string Email { get; set; }
    }
}
