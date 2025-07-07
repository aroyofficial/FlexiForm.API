namespace FlexiForm.API.Internals
{
    /// <summary>
    /// Represents the payload for an OTP operation, including the OTP value and its associated salt.
    /// </summary>
    public class OTPPayload
    {
        /// <summary>
        /// Gets or sets the one-time password (OTP) value in plain text.
        /// </summary>
        public string OTP { get; set; }

        /// <summary>
        /// Gets or sets the cryptographic salt associated with the OTP.
        /// </summary>
        public string Salt { get; set; }
    }
}
