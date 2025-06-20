using FlexiForm.API.Commons.Interfaces;
using System.Security.Claims;

namespace FlexiForm.API.Services.Context
{
    /// <summary>
    /// Provides claim-based access to the currently authenticated user's data
    /// by implementing the <see cref="ICurrentUser"/> interface.
    /// </summary>
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentUser"/> class.
        /// </summary>
        /// <param name="context">The HTTP context accessor to retrieve user claims from the current request.</param>
        public CurrentUser(IHttpContextAccessor context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public int? UserId => GetClaim<int>("user_id");

        /// <inheritdoc />
        public Guid? InternalId => GetClaim<Guid>(ClaimTypes.NameIdentifier);

        /// <inheritdoc />
        public string? Email => GetClaim<string>(ClaimTypes.Email);

        /// <inheritdoc />
        public bool IsAuthenticated =>
            _context.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        /// <summary>
        /// Retrieves a claim value of a specified type from the current user's claims.
        /// </summary>
        /// <typeparam name="T">The type to which the claim value should be converted.</typeparam>
        /// <param name="claimType">The name of the claim to retrieve.</param>
        /// <returns>The claim value converted to the specified type if found and valid; otherwise, <c>null</c>.</returns>
        private T? GetClaim<T>(string claimType)
        {
            var claimValue = _context.HttpContext?.User?.FindFirst(claimType)?.Value;

            if (string.IsNullOrWhiteSpace(claimValue)) return default;

            try
            {
                var targetType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

                if (targetType == typeof(Guid))
                {
                    var parsed = Guid.Parse(claimValue);
                    return (T)(object)parsed;
                }

                return (T)Convert.ChangeType(claimValue, targetType);
            }
            catch
            {
                return default;
            }
        }
    }
}
