namespace FlexiForm.API.Internals
{
    /// <summary>
    /// Represents the payload data associated with a JWT token,
    /// including standard claims like subject, issued time, expiration, and token ID.
    /// </summary>
    public class TokenPayload
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user (mapped to the 'sub' claim in JWT).
        /// </summary>
        public Guid Subject { get; set; }

        /// <summary>
        /// Gets or sets the email address associated with the token (mapped to 'email' claim).
        /// </summary>
        public string Email { get; set; }
    }
}
