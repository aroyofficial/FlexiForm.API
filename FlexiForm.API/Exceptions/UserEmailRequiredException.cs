using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    public class UserEmailRequiredException : BaseException
    {
        public UserEmailRequiredException()
            : base(ErrorCode.UserEmailRequired, "Email address is required.") { }
    }
}
