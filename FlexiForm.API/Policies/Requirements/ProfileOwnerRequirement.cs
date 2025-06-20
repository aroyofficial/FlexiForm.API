using Microsoft.AspNetCore.Authorization;

namespace FlexiForm.API.Policies.Requirements
{
    /// <summary>
    /// Represents a requirement that checks if the currently authenticated user
    /// is the owner of the profile being accessed.
    /// </summary>
    public class ProfileOwnerRequirement : IAuthorizationRequirement
    {
    }
}
