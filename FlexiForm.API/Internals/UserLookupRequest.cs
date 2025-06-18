namespace FlexiForm.API.Internals
{
    /// <summary>
    /// Represents the criteria used to look up a user by either ID or email.
    /// At least one of the properties should be provided.
    /// </summary>
    public class UserLookupRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// Optional if <see cref="Email"/> is provided.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// Optional if <see cref="Id"/> is provided.
        /// </summary>
        public string Email { get; set; }
    }
}
