using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    public class InvalidEmailException : BaseException
    {
        public InvalidEmailException()
            : base(ErrorCode.InvalidEmail, "The provided email address is not valid.") { }
    }
}
