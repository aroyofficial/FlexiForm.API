using Dapper;
using FlexiForm.API.Internals;
using FlexiForm.API.Models;
using FlexiForm.API.Repositories.Interfaces;

namespace FlexiForm.API.Repositories.Implementations
{
    /// <summary>
    /// Provides data access operations related to users.
    /// Implements methods defined in <see cref="IUserRepository"/>.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IBaseRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class with the specified base repository.
        /// </summary>
        /// <param name="repository">The base repository used to execute stored procedures.</param>
        public UserRepository(IBaseRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        public async Task<int> CreateAsync(User user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@firstname", user.FirstName);
            parameters.Add("@lastname", user.LastName);
            parameters.Add("@email", user.Email);
            parameters.Add("@password", user.Password);
            parameters.Add("@gender", Convert.ToByte(user.Gender));

            var procedure = new StoredProcedure()
            {
                Name = "usp_CreateUser",
                Parameters = parameters
            };

            return await _repository.QuerySingleOrDefaultAsync<int>(procedure);
        }

        /// <inheritdoc/>
        public async Task<User> GetAsync(UserLookupRequest request)
        {
            var parameters = new DynamicParameters();

            if (request.Id.HasValue)
            {
                parameters.Add("@id", request.Id.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                parameters.Add("@email", request.Email);
            }

            var procedure = new StoredProcedure()
            {
                Name = "usp_GetUser",
                Parameters = parameters
            };

            return await _repository.QuerySingleOrDefaultAsync<User>(procedure);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(User user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", user.RowId);
            parameters.Add("@firstname", user.FirstName);
            parameters.Add("@lastname", user.LastName);
            parameters.Add("@gender", Convert.ToByte(user.Gender));

            var procedure = new StoredProcedure()
            {
                Name = "usp_UpdateUser",
                Parameters = parameters
            };

            await _repository.ExecuteAsync(procedure);
        }
    }
}
