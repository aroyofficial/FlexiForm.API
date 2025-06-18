using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    /// <summary>
    /// Exception thrown when a user with the specified email already exists in the system.
    /// </summary>
    public class UserAlreadyExistsException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAlreadyExistsException"/> class
        /// with a predefined error code and message indicating duplication.
        /// </summary>
        public UserAlreadyExistsException()
            : base(ErrorCode.UserAlreadyExists, "A user with this email already exists.") { }
    }
}
