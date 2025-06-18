using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    /// <summary>
    /// Exception thrown when a request property exceeds its allowed length.
    /// </summary>
    public class RequestPropertyLengthExceededException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestPropertyLengthExceededException"/> class
        /// with a message indicating which property exceeded the length constraint.
        /// </summary>
        /// <param name="propertyName">The name of the property that exceeded the allowed length.</param>
        public RequestPropertyLengthExceededException(string propertyName)
            : base(ErrorCode.RequestPropertyLengthExceeded, $"The length of '{propertyName}' exceeds the allowed limit.") { }
    }
}
