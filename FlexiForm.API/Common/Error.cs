using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Common
{
    /// <summary>
    /// Represents an error with a descriptive message and a corresponding error code.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Gets or sets the descriptive error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the specific error code associated with the error.
        /// </summary>
        public ErrorCode ErrorCode { get; set; }
    }
}
