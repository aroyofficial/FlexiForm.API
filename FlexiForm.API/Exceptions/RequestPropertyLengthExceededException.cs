using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    public class RequestPropertyLengthExceededException : BaseException
    {
        public RequestPropertyLengthExceededException(string propertyName)
            : base(ErrorCode.RequestPropertyLengthExceeded, $"The length of '{propertyName}' exceeds the allowed limit.") { }
    }
}
