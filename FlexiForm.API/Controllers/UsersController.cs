using FlexiForm.API.Common;
using FlexiForm.API.DTOs.Requests;
using FlexiForm.API.DTOs.Responses;
using FlexiForm.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlexiForm.API.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody]RegistrationRequest request)
        {
            var response = await _service.RegisterAsync(request);
            var apiResponse = new APIResponse<RegistrationResponse>()
            {
                Data = response
            };
            return CreatedAtAction(nameof(Get), new { id = response.Id }, apiResponse);
        }

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
