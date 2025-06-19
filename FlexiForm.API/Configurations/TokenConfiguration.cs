namespace FlexiForm.API.Configurations
{
    /// <summary>
    /// Represents the configuration settings for generating and validating tokens.
    /// </summary>
    public class TokenConfiguration
    {
        /// <summary>
        /// Gets or sets the issuer of the token.
        /// Typically, this is the name or identifier of the application generating the token.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the intended audience of the token.
        /// Used to validate the recipient of the token.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets the expiry time (in minutes) for the token.
        /// Defines how long the token remains valid after issuance.
        /// </summary>
        public int ExpiryMinutes { get; set; }

        /// <summary>
        /// Gets or sets the secret key used to sign and validate the token.
        /// This should be a securely stored and randomly generated string.
        /// </summary>
        public string SecretKey { get; set; }
    }
}
