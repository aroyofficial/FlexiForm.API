using AutoMapper;
using FlexiForm.API.Commons.Interfaces;
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
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private readonly ICurrentUser _currentUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService" /> class.
        /// </summary>
        /// <param name="service">The token generation service.</param>
        /// <param name="repository">The user repository for data access.</param>
        /// <param name="mapper">The AutoMapper instance for object-to-object mapping.</param>
        /// <param name="authRepository">The authentication repository for handling authentication-related data.</param>
        /// <param name="mailService">The mail service for sending emails.</param>
        /// <param name="currentUser">The current user.</param>
        public AuthService(ITokenService service, IUserRepository repository, IMapper mapper, IAuthRepository authRepository, IMailService mailService, ICurrentUser currentUser)
        {
            _service = service;
            _repository = repository;
            _mapper = mapper;
            _authRepository = authRepository;
            _mailService = mailService;
            _currentUser = currentUser;
        }

        /// <inheritdoc/>
        public async Task GenerateOTPAsync(string email)
        {
            var lookUpRequest = _mapper.Map<UserLookupRequest>(email);
            var user = await _repository.GetAsync(lookUpRequest);

            if (user == null)
            {
                throw new UserNotFoundException("email");
            }

            var otp = OTPHelper.Generate();
            var request = new OTPRequest()
            {
                Value = otp.Value,
                Salt = otp.Salt,
                GeneratedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddMinutes(10),
                CreatedBy = user.RowId
            };
            await _authRepository.AddOTPAsync(request);
            var payload = new MailPayload()
            {
                ToEmail = user.Email,
                Macros = new Dictionary<string, string>
                {
                    { "OTP", otp.Value },
                }
            };
            await _mailService.SendForgotPasswordMailAsync(payload);
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

        /// <inheritdoc/>
        public async Task ResetPasswordAsync(ResetPasswordRequest request)
        {
            var (user, otp) = await ValidateAsync(request);
            
            if (!OTPHelper.Verify(otp, request.OTP))
            {
                throw new InvalidOTPException();
            }

            request.NewPassword = PasswordHelper.GetHash(request.NewPassword);
            request.OTP = otp.Value;
            await _authRepository.ResetPasswordAsync(user.RowId, request);

            var payload = new MailPayload()
            {
                ToEmail = user.Email,
                Macros = new Dictionary<string, string>
                {
                    { "UserName", user.FirstName },
                    { "DateTime", _currentUser.LocalTimeNow.ToString("MMMM d, yyyy 'at' h:mm tt") },
                    { "CurrentYear", _currentUser.LocalTimeNow.Year.ToString() },
                }
            };

            await _mailService.SendPasswordChangedAlertMailAsync(payload);
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

        /// <summary>
        /// Validates the incoming reset password request by checking all required fields,
        /// ensuring password strength, and verifying the existence of the user and their associated OTP.
        /// </summary>
        /// <param name="request">
        /// The reset password request containing the user's email, new password, and OTP.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains a tuple with the validated <see cref="User"/>
        /// and the associated <see cref="OTP"/>.
        /// </returns>
        private async Task<(User, OTP)> ValidateAsync(ResetPasswordRequest request)
        {
            if (request == null)
            {
                throw new InvalidRequestException();
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new UserEmailRequiredException();
            }

            if (string.IsNullOrWhiteSpace(request.NewPassword))
            {
                throw new PasswordRequiredException();
            }

            PasswordHelper.CheckStrength(request.NewPassword);

            if (string.IsNullOrWhiteSpace(request.OTP))
            {
                throw new OTPRequiredException();
            }

            var userLookupRequest = _mapper.Map<UserLookupRequest>(request.Email);
            var user = await _repository.GetAsync(userLookupRequest);

            if (user == null)
            {
                throw new UserNotFoundException("email");
            }

            var otp = await _authRepository.GetOTPAsync(user.RowId);
            if (otp == null)
            {
                throw new OTPNotFoundException();
            }

            return (user, otp);
        }
    }
}
