namespace FlexiForm.API.Enumerations
{
    /// <summary>
    /// Represents a list of application-specific error codes used for standardized error handling.
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// A general internal server error. Corresponds to HTTP 500.
        /// </summary>
        InternalServerError = 500,

        /// <summary>
        /// The request is null, malformed, or invalid.
        /// </summary>
        InvalidRequest = 1000,

        /// <summary>
        /// The first name field is missing or empty.
        /// </summary>
        FirstNameRequired = 1001,

        /// <summary>
        /// The last name field is missing or empty.
        /// </summary>
        LastNameRequired = 1002,

        /// <summary>
        /// The email field is missing or empty.
        /// </summary>
        UserEmailRequired = 1003,

        /// <summary>
        /// A request property exceeds the allowed character length.
        /// </summary>
        RequestPropertyLengthExceeded = 1004,

        /// <summary>
        /// The email address provided is not in a valid format.
        /// </summary>
        InvalidEmail = 1005,

        /// <summary>
        /// A user with the given identifier or email already exists.
        /// </summary>
        UserAlreadyExists = 1006,

        /// <summary>
        /// The password field is missing, empty, or consists only of whitespace.
        /// </summary>
        PasswordRequired = 1007,

        /// <summary>
        /// The password does not meet the minimum strength requirements (length, case, digit, special character).
        /// </summary>
        StrongPasswordRequired = 1008,

        /// <summary>
        /// No user found against the given email or identifier.
        /// </summary>
        UserNotFound = 1009
    }
}
