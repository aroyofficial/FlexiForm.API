using FlexiForm.API.Commons;
using FlexiForm.API.Enumerations;
using FlexiForm.API.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace FlexiForm.API.Attributes
{
    /// <summary>
    /// A global exception filter that catches and handles exceptions thrown by API controllers,
    /// and formats them into standardized API responses with appropriate HTTP status codes.
    /// </summary>
    public class APIExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Called when an unhandled exception occurs during the execution of an action.
        /// Converts exceptions into standardized <see cref="APIResponse{T}"/> format with corresponding status codes.
        /// </summary>
        /// <param name="context">The context for the exception.</param>
        public override void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var ex = context.Exception;
            var response = new APIResponse<object>()
            {
                Error = new Error()
                {
                    ErrorCode = ErrorCode.InternalServerError,
                    Message = "An unexpected error occured."
                }
            };

            if (ex is BaseException baseEx)
            {
                response.Error.ErrorCode = baseEx.ErrorCode;
                response.Error.Message = baseEx.Message;
                statusCode = GetHTTPStatusCode(baseEx);
            }

            context.Result = new ObjectResult(response)
            {
                StatusCode = Convert.ToInt16(statusCode)
            };

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Maps a custom <see cref="BaseException"/>'s <see cref="ErrorCode"/> to the appropriate <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="ex">The base exception containing the application-specific error code.</param>
        /// <returns>The corresponding <see cref="HttpStatusCode"/> for the given error code.</returns>
        private static HttpStatusCode GetHTTPStatusCode(BaseException ex)
        {
            return ex.ErrorCode switch
            {
                ErrorCode.FirstNameRequired => HttpStatusCode.BadRequest,
                ErrorCode.LastNameRequired => HttpStatusCode.BadRequest,
                ErrorCode.UserEmailRequired => HttpStatusCode.BadRequest,
                ErrorCode.RequestPropertyLengthExceeded => HttpStatusCode.BadRequest,
                ErrorCode.InvalidEmail => HttpStatusCode.BadRequest,
                ErrorCode.InvalidRequest => HttpStatusCode.BadRequest,
                ErrorCode.UserAlreadyExists => HttpStatusCode.Conflict,
                ErrorCode.InternalServerError => HttpStatusCode.InternalServerError,
                ErrorCode.PasswordRequired => HttpStatusCode.BadRequest,
                ErrorCode.StrongPasswordRequired => HttpStatusCode.BadRequest,
                ErrorCode.UserNotFound => HttpStatusCode.NotFound,
                ErrorCode.InvalidCredentials => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.BadRequest
            };
        }
    }
}
