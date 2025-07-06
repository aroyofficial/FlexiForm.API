using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an OTP is not found for the specified user or email.
    /// </summary>
    public class OTPNotFoundException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OTPNotFoundException"/> class
        /// with a predefined error code and message indicating that the OTP was not found.
        /// </summary>
        public OTPNotFoundException()
            : base(ErrorCode.OTPNotFound, "OTP not found for the user with the provided email.")
        {
        }
    }
}
