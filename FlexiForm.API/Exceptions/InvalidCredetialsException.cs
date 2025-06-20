using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    /// <summary>
    /// Exception thrown when user authentication fails due to invalid email or password.
    /// </summary>
    public class InvalidCredentialsException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCredentialsException"/> class.
        /// </summary>
        public InvalidCredentialsException()
            : base(ErrorCode.InvalidCredentials, "Invalid email or password.")
        {
        }
    }
}
