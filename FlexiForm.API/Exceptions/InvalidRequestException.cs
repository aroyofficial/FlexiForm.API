using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    /// <summary>
    /// Exception thrown when the incoming request object is null or invalid.
    /// </summary>
    public class InvalidRequestException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRequestException"/> class
        /// with a predefined error code and message indicating the request is invalid.
        /// </summary>
        public InvalidRequestException()
            : base(ErrorCode.InvalidRequest, "The request object is null or invalid.") { }
    }
}
