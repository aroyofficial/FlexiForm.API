namespace FlexiForm.API.Commons.Interfaces
{
    /// <summary>
    /// Defines an abstraction for accessing the currently authenticated user's claims.
    /// </summary>
    public interface ICurrentUser
    {
        /// <summary>
        /// Gets the integer user ID from the JWT claim (typically from "user_id").
        /// </summary>
        int? UserId { get; }

        /// <summary>
        /// Gets the unique internal identifier (GUID) of the user from the JWT claim (typically from "rowId").
        /// </summary>
        Guid? InternalId { get; }

        /// <summary>
        /// Gets the email address of the authenticated user from the JWT claim.
        /// </summary>
        string? Email { get; }

        /// <summary>
        /// Gets a value indicating whether the current user is authenticated.
        /// </summary>
        bool IsAuthenticated { get; }
    }
}
