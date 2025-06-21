using AutoMapper;
using FlexiForm.API.DTOs.Requests;
using FlexiForm.API.Models;

namespace FlexiForm.API.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping registration request data to the <see cref="User"/> model.
    /// </summary>
    public class UserProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfile"/> class.
        /// </summary>
        public UserProfile()
        {
            MapRegistrationRequest();
            MapUserUpdateRequest();
        }

        /// <summary>
        /// Configures the mapping from <see cref="RegistrationRequest"/> to <see cref="User"/>.
        /// </summary>
        private void MapRegistrationRequest()
        {
            CreateMap<RegistrationRequest, User>()
                .ForMember(dest => dest.FirstName, mapper => mapper.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, mapper => mapper.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, mapper => mapper.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, mapper => mapper.MapFrom(src => src.Password))
                .ForMember(dest => dest.Gender, mapper => mapper.MapFrom(src => src.Gender));
        }

        /// <summary>
        /// Configures the AutoMapper mapping between <see cref="UserUpdateRequest"/> and <see cref="User"/>.
        /// Maps the FirstName, LastName, and Gender properties from the request to the corresponding entity fields.
        /// </summary>
        private void MapUserUpdateRequest()
        {
            CreateMap<UserUpdateRequest, User>()
                .ForMember(dest => dest.FirstName, mapper => mapper.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, mapper => mapper.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Gender, mapper => mapper.MapFrom(src => src.Gender));
        }
    }
}
