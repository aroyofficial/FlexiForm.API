using FlexiForm.API.Enumerations;

namespace FlexiForm.API.Exceptions
{
    public class LastNameRequiredException : BaseException
    {
        public LastNameRequiredException()
            : base(ErrorCode.LastNameRequired, "Last name is required.") { }
    }
}
