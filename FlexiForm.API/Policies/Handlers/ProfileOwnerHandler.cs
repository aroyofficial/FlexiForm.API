using FlexiForm.API.Commons.Interfaces;
using FlexiForm.API.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace FlexiForm.API.Policies.Handlers
{
    /// <summary>
    /// Authorization handler that verifies whether the currently authenticated user
    /// is the owner of the profile being accessed, based on route data.
    /// </summary>
    public class ProfileOwnerHandler : AuthorizationHandler<ProfileOwnerRequirement>
    {
        private readonly IHttpContextAccessor _context;
        private readonly ICurrentUser _currentUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileOwnerHandler"/> class.
        /// </summary>
        /// <param name="context">The HTTP context accessor used to retrieve route values.</param>
        /// <param name="currentUser">The current authenticated user's claims-based identity.</param>
        public ProfileOwnerHandler(IHttpContextAccessor context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        /// <inheritdoc/>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProfileOwnerRequirement requirement)
        {
            var routeData = _context.HttpContext?.GetRouteData();

            if (int.TryParse(routeData?.Values["id"]?.ToString(), out int userIdFromRoute))
            {
                if (_currentUser.UserId.HasValue && _currentUser.UserId.Value == userIdFromRoute)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
