using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when the OTP provided by the user is invalid.
    /// </summary>
    /// <remarks>
    /// This exception is typically thrown during operations like password reset or verification,
    /// where a one-time password (OTP) is required and the provided value does not match the expected OTP.
    /// </remarks>
    public class InvalidOTPException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidOTPException"/> class
        /// with a predefined error code and message indicating that the OTP is invalid.
        /// </summary>
        public InvalidOTPException()
            : base(ErrorCode.InvalidOTP, "The OTP you entered is invalid. Please try again.")
        {
        }
    }
}
