using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Common
{
    /// <summary>
    /// Represents a standardized API response wrapper containing the result, status, message, and error code.
    /// </summary>
    /// <typeparam name="T">The type of the data being returned in the response.</typeparam>
    public class APIResponse<T>
    {
        /// <summary>
        /// Gets or sets the data returned by the API call.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Indicates whether the API call was successful.
        /// </summary>
        public bool Success => Error == null;

        /// <summary>
        /// Gets or sets the descriptive error details.
        /// </summary>
        public Error Error { get; set; }
    }
}
