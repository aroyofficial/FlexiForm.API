using FlexiForm.API.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace FlexiForm.API.Controllers
{
    /// <summary>
    /// Serves as the base class for all API controllers in the application.
    /// Inherits from <see cref="ControllerBase"/> to provide common API controller behavior.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [APIExceptionFilter]
    public class BaseController : ControllerBase
    {
    }
}
