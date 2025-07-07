namespace FlexiForm.API.DTOs.Requests
{
    /// <summary>
    /// Represents the request payload for resetting a user's password using an OTP.
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>
        /// Gets or sets the email address of the user requesting the password reset.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the new password that the user wants to set.
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the one-time password (OTP) provided to verify the user's identity.
        /// </summary>
        public string OTP { get; set; }
    }
}
