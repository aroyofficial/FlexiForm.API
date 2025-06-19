using FlexiForm.API.Commons;
using FlexiForm.API.DTOs.Requests;
using FlexiForm.API.DTOs.Responses;
using FlexiForm.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexiForm.API.Controllers
{
    /// <summary>
    /// Controller responsible for handling user authentication requests.
    /// </summary>
    public class AuthController : BaseController
    {
        private readonly IAuthService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="service">The authentication service to handle login logic.</param>
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        /// <summary>
        /// Authenticates a user and returns a JWT access token with user details.
        /// </summary>
        /// <param name="request">The login request containing email and password.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing an <see cref="APIResponse{T}"/> with 
        /// the login result if successful, or an appropriate error response.
        /// </returns>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _service.LoginAsync(request);
            var apiResponse = new APIResponse<LoginResponse>
            {
                Data = response
            };
            return Ok(apiResponse);
        }
    }
}
