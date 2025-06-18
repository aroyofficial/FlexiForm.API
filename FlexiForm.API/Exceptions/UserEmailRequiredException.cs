using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    /// <summary>
    /// Exception thrown when the email address is missing in a user-related request.
    /// </summary>
    public class UserEmailRequiredException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserEmailRequiredException"/> class
        /// with a predefined error code and message indicating the email address is required.
        /// </summary>
        public UserEmailRequiredException()
            : base(ErrorCode.UserEmailRequired, "Email address is required.") { }
    }
}
