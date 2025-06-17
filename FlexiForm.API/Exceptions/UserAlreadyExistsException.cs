using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    public class UserAlreadyExistsException : BaseException
    {
        public UserAlreadyExistsException()
            : base(ErrorCode.UserAlreadyExists, "A user with this email already exists.") { }
    }
}
