namespace FlexiForm.API.Enumerations
{
    /// <summary>
    /// Represents the possible statuses for an OTP (One-Time Password).
    /// </summary>
    public enum OTPStatus
    {
        /// <summary>
        /// Indicates that the OTP has expired and is no longer valid.
        /// </summary>
        Expired,

        /// <summary>
        /// Indicates that the OTP is newly generated and has not yet been used.
        /// </summary>
        New,

        /// <summary>
        /// Indicates that the OTP has been consumed or used.
        /// </summary>
        Consumed
    }
}
