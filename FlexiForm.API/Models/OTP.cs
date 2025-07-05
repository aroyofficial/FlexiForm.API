using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Models
{
    /// <summary>
    /// Represents a One-Time Password (OTP) entity, containing its value, status, and lifecycle timestamps.
    /// </summary>
    public class OTP : BaseModel
    {
        /// <summary>
        /// Gets or sets the current status of the OTP.
        /// </summary>
        public OTPStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the OTP value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the OTP was generated.
        /// </summary>
        public DateTime GeneratedAt { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the OTP will expire.
        /// </summary>
        public DateTime ExpiredAt { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the OTP was consumed, if applicable.
        /// </summary>
        public DateTime? ConsumedAt { get; set; }
    }
}
