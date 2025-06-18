using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    /// <summary>
    /// Exception thrown when a password is not provided.
    /// </summary>
    public class PasswordRequiredException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordRequiredException"/> class.
        /// </summary>
        public PasswordRequiredException()
            : base(ErrorCode.PasswordRequired, "Password is required.") { }
    }
}
