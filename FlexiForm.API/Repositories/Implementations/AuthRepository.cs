using Dapper;
using FlexiForm.API.DTOs.Requests;
using FlexiForm.API.Internals;
using FlexiForm.API.Repositories.Interfaces;

namespace FlexiForm.API.Repositories.Implementations
{
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

        /// <summary>
        /// Adds a new OTP (One-Time Password) record to the database asynchronously.
        /// </summary>
        /// <param name="request">The OTP request data containing the value, generation time, expiration time, and creator information.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddOTPAsync(OTPRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@value", request.Value);
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
    }
}
