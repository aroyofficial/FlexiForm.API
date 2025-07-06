using Dapper;
using FlexiForm.API.DTOs.Requests;
using FlexiForm.API.Internals;
using FlexiForm.API.Models;
using FlexiForm.API.Repositories.Interfaces;

namespace FlexiForm.API.Repositories.Implementations
{0
    /// <summary>
    /// Provides the implementation for authentication-related data operations.
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        private readonly IBaseRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthRepository"/> class.
        /// </summary>
        /// <param name="repository">The base repository used for database operations.</param>
        public AuthRepository(IBaseRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        public async Task AddOTPAsync(OTPRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@value", request.Value);
            parameters.Add("@salt", request.Salt);
            parameters.Add("@generatedat", request.GeneratedAt);
            parameters.Add("@expiredat", request.ExpiredAt);
            parameters.Add("@createdby", request.CreatedBy);

            var procedure = new StoredProcedure()
            {
                Name = "usp_AddOTP",
                Parameters = parameters
            };

            await _repository.ExecuteAsync(procedure);
        }

        /// <inheritdoc/>
        public async Task<OTP> GetOTPAsync(Guid userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@userid", userId);

            var procedure = new StoredProcedure()
            {
                Name = "usp_GetOTP",
                Parameters = parameters
            };

            return await _repository.QuerySingleOrDefaultAsync<OTP>(procedure);
        }

        /// <inheritdoc/>
        public async Task ResetPasswordAsync(Guid userId, ResetPasswordRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@userid", userId);
            parameters.Add("@password", request.NewPassword);
            parameters.Add("@otp", request.OTP);

            var procedure = new StoredProcedure()
            {
                Name = "usp_ResetPassword",
                Parameters = parameters
            };

            await _repository.ExecuteAsync(procedure);
        }
    }
}
