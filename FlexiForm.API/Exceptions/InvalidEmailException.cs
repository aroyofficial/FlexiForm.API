using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    /// <summary>
    /// Exception thrown when the provided email address is invalid.
    /// </summary>
    public class InvalidEmailException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEmailException"/> class
        /// with a predefined error code and message indicating the email is not valid.
        /// </summary>
        public InvalidEmailException()
            : base(ErrorCode.InvalidEmail, "The provided email address is not valid.") { }
    }
}
