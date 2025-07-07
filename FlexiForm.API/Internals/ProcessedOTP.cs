namespace FlexiForm.API.Internals
{
    /// <summary>
    /// Represents the newly generated OTP.
    /// </summary>
    public class ProcessedOTP
    {
        /// <summary>
        /// Gets or sets the plain text one-time password (OTP) value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the SHA-256 hashed value of the OTP combined with the salt.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Gets or sets the cryptographic salt used during OTP hashing.
        /// </summary>
        public string Salt { get; set; }
    }
}
