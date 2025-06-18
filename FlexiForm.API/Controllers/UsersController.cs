using FlexiForm.API.Commons;
using FlexiForm.API.DTOs.Requests;
using FlexiForm.API.DTOs.Responses;
using FlexiForm.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlexiForm.API.Controllers
{
    /// <summary>
    /// Controller responsible for handling user-related operations such as registration and retrieval.
    /// </summary>
    public class UsersController : BaseController
    {
        private readonly IUserService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="service">The user service instance to be used for user operations.</param>
        public UsersController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Registers a new user with the provided registration details.
        /// </summary>
        /// <param name="request">The registration request containing user information.</param>
        /// <returns>
        /// A <see cref="CreatedAtActionResult"/> containing the registered user's identifier and details.
        /// </returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            var response = await _service.RegisterAsync(request);
            var apiResponse = new APIResponse<RegistrationResponse>()
            {
                Data = response
            };
            return CreatedAtAction(nameof(Get), new { id = response.Id }, apiResponse);
        }

        /// <summary>
        /// Retrieves user details for the specified user ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>
        /// An <see cref="OkObjectResult"/> containing the user details, or a <see cref="NotFoundResult"/> if not found.
        /// </returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.GetAsync(id);
            var apiResponse = new APIResponse<UserResponse>()
            {
                Data = response
            };
            return Ok(apiResponse);
        }
    }
}
