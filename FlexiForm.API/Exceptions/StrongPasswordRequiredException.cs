using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    /// <summary>
    /// Exception thrown when the provided password does not meet strength requirements.
    /// </summary>
    public class StrongPasswordRequiredException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StrongPasswordRequiredException"/> class.
        /// </summary>
        public StrongPasswordRequiredException()
            : base(ErrorCode.StrongPasswordRequired, "Password must be at least 8 characters and include uppercase, lowercase, digit, and special character.") { }
    }
}
