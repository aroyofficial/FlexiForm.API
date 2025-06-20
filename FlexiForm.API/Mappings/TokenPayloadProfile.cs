using AutoMapper;
using FlexiForm.API.Internals;
using FlexiForm.API.Models;

namespace FlexiForm.API.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping <see cref="User"/> to <see cref="TokenPayload"/>.
    /// </summary>
    public class TokenPayloadProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenPayloadProfile"/> class.
        /// </summary>
        public TokenPayloadProfile()
        {
            MapUser();
        }

        /// <summary>
        /// Defines the mapping configuration from <see cref="User"/> to <see cref="TokenPayload"/>.
        /// </summary>
        private void MapUser()
        {
            CreateMap<User, TokenPayload>()
                .ForMember(dest => dest.UserId, mapper => mapper.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, mapper => mapper.MapFrom(src => src.Email))
                .ForMember(dest => dest.Subject, mapper => mapper.MapFrom(src => src.RowId));
        }
    }
}
