using FlexiForm.API.Commons;
using FlexiForm.API.DTOs.Requests;
using FlexiForm.API.DTOs.Responses;
using FlexiForm.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthorizationPolicy = FlexiForm.API.Constants.AuthorizationPolicy;

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
        [AllowAnonymous]
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
        [Authorize(Policy = AuthorizationPolicy.ProfileOwner)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var response = await _service.GetAsync(id);
            var apiResponse = new APIResponse<UserResponse>()
            {
                Data = response
            };
            return Ok(apiResponse);
        }

        /// <summary>
        /// Updates an existing user's information based on the provided user ID and update request.
        /// </summary>
        /// <param name="id">The unique identifier of the user to update.</param>
        /// <param name="request">The <see cref="UserUpdateRequest"/> object containing the updated user details.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing a success response with the updated <see cref="UserResponse"/> data.
        /// Returns <c>200 OK</c> if the update is successful.
        /// </returns>
        [Authorize(Policy = AuthorizationPolicy.ProfileOwner)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UserUpdateRequest request)
        {
            var response = await _service.UpdateAsync(id, request);
            var apiResponse = new APIResponse<UserResponse>()
            {
                Data = response
            };
            return Ok(apiResponse);
        }

        /// <summary>
        /// Deletes a user profile based on the specified ID.
        /// </summary>
        /// <param name="id">The ID of the profile to be deleted.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> indicating the result of the delete operation.  
        /// A 200 OK response with an empty <see cref="APIResponse{T}"/> object is returned if the deletion is successful.
        /// </returns>
        [Authorize(Policy = AuthorizationPolicy.ProfileOwner)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _service.DeleteAsync(id);
            var apiResponse = new APIResponse<object>();
            return Ok(apiResponse);
        }
    }
}
