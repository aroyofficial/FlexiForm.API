using AutoMapper;
using FlexiForm.API.Commons.Interfaces;
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
            MapString();
            MapICurrentUser();
        }

        /// <summary>
        /// Configures mapping from <see cref="LoginRequest"/> to <see cref="UserLookupRequest"/>.
        /// </summary>
        private void MapLoginRequest()
        {
            CreateMap<LoginRequest, UserLookupRequest>()
                .ForMember(dest => dest.Email, member => member.MapFrom(src => src.Email));
        }

        /// <summary>
        /// Configures mapping from a <see cref="string"/> value to a <see cref="UserLookupRequest"/> object.
        /// </summary>
        private void MapString()
        {
            CreateMap<string, UserLookupRequest>()
                .ForMember(dest => dest.Email, member => member.MapFrom(src => src));
        }

        /// <summary>
        /// Configures the mapping from <see cref="ICurrentUser"/> to <see cref="UserLookupRequest"/>.
        /// </summary>
        private void MapICurrentUser()
        {
            CreateMap<ICurrentUser, UserLookupRequest>()
                .ForMember(dest => dest.Id, member => member.MapFrom(src => src.InternalId));
        }
    }
}
