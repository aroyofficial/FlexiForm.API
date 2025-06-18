using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    /// <summary>
    /// Exception thrown when the last name is missing in a request.
    /// </summary>
    public class LastNameRequiredException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LastNameRequiredException"/> class
        /// with a predefined error code and message indicating the last name is required.
        /// </summary>
        public LastNameRequiredException()
            : base(ErrorCode.LastNameRequired, "Last name is required.") { }
    }
}
