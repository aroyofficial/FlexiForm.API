namespace FlexiForm.API.Constants
{
    /// <summary>
    /// Contains constant policy names used for authorization throughout the application.
    /// </summary>
    public static class AuthorizationPolicy
    {
        /// <summary>
        /// Policy that restricts access to the owner of the profile being accessed.
        /// </summary>
        public const string ProfileOwner = "ProfileOwner";
    }
}
