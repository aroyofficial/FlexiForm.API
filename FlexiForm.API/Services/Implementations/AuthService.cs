using AutoMapper;
using FlexiForm.API.DTOs.Requests;
using FlexiForm.API.DTOs.Responses;
using FlexiForm.API.Exceptions;
using FlexiForm.API.Helpers;
using FlexiForm.API.Internals;
using FlexiForm.API.Models;
using FlexiForm.API.Repositories.Interfaces;
using FlexiForm.API.Services.Interfaces;

namespace FlexiForm.API.Services.Implementations
{
    /// <summary>
    /// Provides authentication-related services such as login and credential validation.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly ITokenService _service;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="service">The token generation service.</param>
        /// <param name="repository">The user repository for data access.</param>
        /// <param name="mapper">The AutoMapper instance for object-to-object mapping.</param>
        public AuthService(ITokenService service, IUserRepository repository, IMapper mapper)
        {
            _service = service;
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Authenticates the user based on the provided login request.
        /// </summary>
        /// <param name="request">The login request containing email and password.</param>
        /// <returns>A <see cref="LoginResponse"/> containing access token and user info.</returns>
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await ValidateAsync(request);

            if (!PasswordHelper.Verify(request.Password, user.Password))
            {
                throw new InvalidCredentialsException();
            }

            var payload = _mapper.Map<TokenPayload>(user);
            var token = _service.Generate(payload);
            var response = _mapper.Map<LoginResponse>(token);
            response = _mapper.Map(user, response, typeof(User), typeof(LoginResponse)) as LoginResponse;
            return response;
        }

        /// <summary>
        /// Validates the login request and retrieves the corresponding user from the repository.
        /// </summary>
        /// <param name="request">The login request to validate.</param>
        /// <returns>The <see cref="User"/> object if validation is successful.</returns>
        private async Task<User> ValidateAsync(LoginRequest request)
        {
            if (request == null)
            {
                throw new InvalidRequestException();
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new UserEmailRequiredException();
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new PasswordRequiredException();
            }

            var lookupRequest = _mapper.Map<UserLookupRequest>(request);
            var user = await _repository.GetAsync(lookupRequest);

            if (user == null)
            {
                throw new InvalidCredentialsException();
            }

            return user;
        }
    }
}
