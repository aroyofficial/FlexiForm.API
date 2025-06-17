using AutoMapper;
using FlexiForm.API.DTOs.Requests;
using FlexiForm.API.DTOs.Responses;
using FlexiForm.API.Exceptions;
using FlexiForm.API.Helpers;
using FlexiForm.API.Internals;
using FlexiForm.API.Models;
using FlexiForm.API.Repositories.Interfaces;
using FlexiForm.API.Services.Interfaces;
using System.Net.Mail;

namespace FlexiForm.API.Services.Implementations
{
    /// <summary>
    /// Provides user-related service operations, such as registration.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="repository">The user repository for data access.</param>
        /// <param name="mapper">The AutoMapper instance used for object mapping.</param>
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<UserResponse> GetAsync(int id)
        {
            var lookupRequest = new UserLookupRequest() { Id = id };
            var user = await _repository.GetAsync(lookupRequest);
            
            if (user == null)
            {
                throw new UserNotFoundException("identifier");
            }

            var response = _mapper.Map<UserResponse>(user);
            return response;
        }

        /// <inheritdoc/>
        public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
        {
            await ValidateAsync(request);
            request.Password = PasswordHelper.GetHash(request.Password);
            var user = _mapper.Map<User>(request);
            var id = await _repository.CreateAsync(user);
            var response = _mapper.Map<RegistrationResponse>(request);
            response.Id = id;
            return response;
        }

        /// <summary>
        /// Validates the <see cref="RegistrationRequest"/> object by checking required fields,
        /// length constraints, email format, and uniqueness.
        /// </summary>
        /// <param name="request">The registration request to validate.</param>
        private async Task ValidateAsync(RegistrationRequest request)
        {
            if (request == null)
            {
                throw new InvalidRequestException();
            }

            if (string.IsNullOrWhiteSpace(request.FirstName))
            {
                throw new FirstNameRequiredException();
            }

            if (string.IsNullOrWhiteSpace(request.LastName))
            {
                throw new LastNameRequiredException();
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new UserEmailRequiredException();
            }

            if (request.FirstName.Length > 128)
            {
                throw new RequestPropertyLengthExceededException("firstname");
            }

            if (request.LastName.Length > 128)
            {
                throw new RequestPropertyLengthExceededException("lastname");
            }

            if (request.Email.Length > 256)
            {
                throw new RequestPropertyLengthExceededException("email");
            }

            if (new MailAddress(request.Email).Address != request.Email)
            {
                throw new InvalidEmailException();
            }

            PasswordHelper.CheckStrength(request.Password);

            var lookupRequest = new UserLookupRequest() { Email = request.Email };
            var user = await _repository.GetAsync(lookupRequest);

            if (user != null)
            {
                throw new UserAlreadyExistsException();
            }
        }
    }
}
