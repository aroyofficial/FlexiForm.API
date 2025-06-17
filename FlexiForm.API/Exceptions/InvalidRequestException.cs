using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    public class InvalidRequestException : BaseException
    {
        public InvalidRequestException()
            : base(ErrorCode.InvalidRequest, "The request object is null or invalid.") { }
    }
}
