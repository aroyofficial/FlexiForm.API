using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    /// <summary>
    /// Exception thrown when the first name is missing in a request.
    /// </summary>
    public class FirstNameRequiredException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirstNameRequiredException"/> class
        /// with a predefined error code and message.
        /// </summary>
        public FirstNameRequiredException()
            : base(ErrorCode.FirstNameRequired, "First name is required.") { }
    }
}
