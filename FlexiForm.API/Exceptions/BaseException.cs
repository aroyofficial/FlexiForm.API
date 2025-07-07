using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    /// <summary>
    /// Represents the base class for all custom exceptions in the application.
    /// </summary>
    public abstract class BaseException : Exception
    {
        /// <summary>
        /// Gets the specific error code associated with the exception.
        /// </summary>
        public ErrorCode ErrorCode { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class with the specified error code and message.
        /// </summary>
        /// <param name="errorCode">The application-specific error code that identifies the type of error.</param>
        /// <param name="message">A human-readable message that describes the error.</param>
        protected BaseException(ErrorCode errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
