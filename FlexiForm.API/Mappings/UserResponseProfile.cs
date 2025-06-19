using AutoMapper;
using FlexiForm.API.DTOs.Responses;
using FlexiForm.API.Models;

namespace FlexiForm.API.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping <see cref="User"/> model to <see cref="UserResponse"/> DTO.
    /// </summary>
    public class UserResponseProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserResponseProfile"/> class.
        /// </summary>
        public UserResponseProfile()
        {
            MapUser();
        }

        /// <summary>
        /// Configures the mapping from <see cref="User"/> to <see cref="UserResponse"/>.
        /// </summary>
        private void MapUser()
        {
            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.Id, mapper => mapper.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, mapper => mapper.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, mapper => mapper.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, mapper => mapper.MapFrom(src => src.Email))
                .ForMember(dest => dest.Gender, mapper => mapper.MapFrom(src => src.Gender));
        }
    }
}
