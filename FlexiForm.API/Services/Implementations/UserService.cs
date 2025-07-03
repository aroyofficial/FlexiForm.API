using AutoMapper;
using Azure.Core;
using FlexiForm.API.Commons.Interfaces;
using FlexiForm.API.DTOs.Requests;
using FlexiForm.API.DTOs.Responses;
using FlexiForm.API.Enumerations;
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
        private readonly ICurrentUser _currentUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="repository">The user repository for data access.</param>
        /// <param name="mapper">The AutoMapper instance used for object mapping.</param>
        /// <param name="currentUser">The current user context providing information about the authenticated user.</param>
        public UserService(IUserRepository repository, IMapper mapper, ICurrentUser currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        /// <inheritdoc/>
        public async Task<UserResponse> GetAsync(int id)
        {
            var lookupRequest = _mapper.Map<UserLookupRequest>(_currentUser);
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

        /// <inheritdoc/>
        public async Task<UserResponse> UpdateAsync(int id, UserUpdateRequest request)
        {
            Validate(request);
            var userLookupRequest = _mapper.Map<UserLookupRequest>(id);
            var user = await _repository.GetAsync(userLookupRequest);

            if (user == null)
            {
                throw new UserNotFoundException("identifier");
            }

            user = _mapper.Map(request, user, typeof(UserUpdateRequest), typeof(User)) as User;
            await _repository.UpdateAsync(user);
            var response = _mapper.Map<User, UserResponse>(user);
            return response;
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            var lookupRequest = _mapper.Map<UserLookupRequest>(_currentUser);
            var user = await _repository.GetAsync(lookupRequest);

            if (user == null)
            {
                throw new UserNotFoundException("identifier");
            }
            else
            {
                await _repository.DeleteAsync(_currentUser.InternalId.Value);
            }
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

            ValidateName(request.FirstName, request.LastName);
            ValidateEmail(request.Email);
            ValidateGender(request.Gender);

            PasswordHelper.CheckStrength(request.Password);

            var lookupRequest = _mapper.Map<UserLookupRequest>(request.Email);
            var user = await _repository.GetAsync(lookupRequest);

            if (user != null)
            {
                throw new UserAlreadyExistsException();
            }
        }

        /// <summary>
        /// Validates the provided <see cref="UserUpdateRequest"/> object, including first name, last name, and gender.
        /// Throws specific exceptions if validation fails.
        /// </summary>
        /// <param name="request">The user update request to validate.</param>
        private static void Validate(UserUpdateRequest request)
        {
            if (request == null)
            {
                throw new InvalidRequestException();
            }

            ValidateName(request.FirstName, request.LastName);
            ValidateGender(request.Gender);
        }

        /// <summary>
        /// Validates the first and last name for null, whitespace, and length constraints.
        /// Throws specific exceptions if validation fails.
        /// </summary>
        /// <param name="firstName">The first name to validate.</param>
        /// <param name="lastName">The last name to validate.</param>
        private static void ValidateName(string? firstName, string? lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new FirstNameRequiredException();
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new LastNameRequiredException();
            }

            if (firstName.Length > 128)
            {
                throw new RequestPropertyLengthExceededException("firstname");
            }

            if (lastName.Length > 128)
            {
                throw new RequestPropertyLengthExceededException("lastname");
            }
        }

        /// <summary>
        /// Validates the email address for null, whitespace, length, and proper format.
        /// Throws specific exceptions if validation fails.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        private static void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new UserEmailRequiredException();
            }

            if (email.Length > 256)
            {
                throw new RequestPropertyLengthExceededException("email");
            }

            if (new MailAddress(email).Address != email)
            {
                throw new InvalidEmailException();
            }
        }

        /// <summary>
        /// Validates whether the provided gender value is a defined enum value.
        /// Throws an exception if the gender is invalid.
        /// </summary>
        /// <param name="gender">The gender value to validate.</param>
        private static void ValidateGender(Gender gender)
        {
            if (gender == Gender.None)
            {
                throw new GenderRequiredException();
            }

            if (!Enum.IsDefined(typeof(Gender), gender))
            {
                throw new InvalidGenderException(gender);
            }
        }
    }
}
