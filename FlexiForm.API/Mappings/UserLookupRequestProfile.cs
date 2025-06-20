using AutoMapper;
using FlexiForm.API.DTOs.Requests;
using FlexiForm.API.Internals;

namespace FlexiForm.API.Mappings
{
    /// <summary>
    /// Defines AutoMapper configuration for mapping <see cref="LoginRequest"/> to <see cref="UserLookupRequest"/>.
    /// </summary>
    public class UserLookupRequestProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserLookupRequestProfile"/> class.
        /// </summary>
        public UserLookupRequestProfile()
        {
            MapLoginRequest();
        }

        /// <summary>
        /// Configures mapping from <see cref="LoginRequest"/> to <see cref="UserLookupRequest"/>.
        /// </summary>
        private void MapLoginRequest()
        {
            CreateMap<LoginRequest, UserLookupRequest>()
                .ForMember(dest => dest.Email, member => member.MapFrom(src => src.Email));
        }
    }
}
