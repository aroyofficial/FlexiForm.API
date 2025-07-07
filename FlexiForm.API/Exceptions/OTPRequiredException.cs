using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an OTP is required but not provided.
    /// </summary>
    public class OTPRequiredException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OTPRequiredException"/> class
        /// with a predefined error code and message indicating that an OTP is required.
        /// </summary>
        public OTPRequiredException()
            : base(ErrorCode.OTPRequired, "OTP is required for this operation.")
        {
        }
    }
}
