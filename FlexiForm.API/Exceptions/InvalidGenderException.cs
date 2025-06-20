using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an invalid gender value is provided.
    /// </summary>
    public class InvalidGenderException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidGenderException"/> class
        /// with the specified invalid gender.
        /// </summary>
        /// <param name="gender">The gender value that caused the exception.</param>
        public InvalidGenderException(Gender gender)
            : base(ErrorCode.InvalidGender, $"The given gender '{gender}' is not valid.")
        {
        }
    }
}
