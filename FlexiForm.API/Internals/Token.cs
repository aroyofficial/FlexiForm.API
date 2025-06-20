namespace FlexiForm.API.Internals
{
    /// <summary>
    /// Represents an access token and its expiration details.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Gets or sets the token value (typically a JWT).
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the token was issued.
        /// </summary>
        public DateTime IssuedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time at which the token expires.
        /// </summary>
        public DateTime ExpiresAt { get; set; }
    }
}
