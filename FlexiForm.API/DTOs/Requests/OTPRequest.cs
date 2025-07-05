namespace FlexiForm.API.DTOs.Requests
{
    /// <summary>
    /// Represents the request data required to create a new OTP (One-Time Password).
    /// </summary>
    public class OTPRequest
    {
        /// <summary>
        /// Gets or sets the value of the OTP.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the timestamp indicating when the OTP was generated.
        /// </summary>
        public DateTime GeneratedAt { get; set; }

        /// <summary>
        /// Gets or sets the timestamp indicating when the OTP will expire.
        /// </summary>
        public DateTime ExpiredAt { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who created the OTP.
        /// </summary>
        public Guid CreatedBy { get; set; }
    }
}
