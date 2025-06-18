using AutoMapper;
using FlexiForm.API.DTOs.Requests;
using FlexiForm.API.DTOs.Responses;
using FlexiForm.API.Models;

namespace FlexiForm.API.Mappings
{
    /// <summary>
    /// Defines the mapping configuration for user-related objects using AutoMapper.
    /// </summary>
    public class UserMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMappingProfile"/> class.
        /// Configures mappings between user request and response DTOs.
        /// </summary>
        public UserMappingProfile()
        {
            CreateMap<RegistrationRequest, RegistrationResponse>();
            CreateMap<RegistrationRequest, User>();
            CreateMap<User, UserResponse>();
        }
    }
}
