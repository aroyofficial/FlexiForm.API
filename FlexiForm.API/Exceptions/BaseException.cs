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

        protected BaseException(ErrorCode errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
