using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when the gender value is not provided in a request where it is required.
    /// </summary>
    public class GenderRequiredException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenderRequiredException"/> class
        /// with a predefined error message indicating that gender is required.
        /// </summary>
        public GenderRequiredException()
            : base(ErrorCode.GenderRequired, "Gender is required and must be provided.")
        {
        }
    }
}
