using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    public class FirstNameRequiredException : BaseException
    {
        public FirstNameRequiredException()
            : base(ErrorCode.FirstNameRequired, "First name is required.") { }
    }
}
