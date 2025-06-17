using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    /// <summary>
    /// Exception thrown when a user is not found based on a specified field.
    /// </summary>
    public class UserNotFoundException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotFoundException"/> class
        /// with the specified field used for lookup.
        /// </summary>
        /// <param name="field">The name of the field used to search for the user.</param>
        public UserNotFoundException(string field)
            : base(ErrorCode.UserNotFound, $"User not found with the given {field}")
        {
        }
    }
}
