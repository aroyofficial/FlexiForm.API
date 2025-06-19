using AutoMapper;
using FlexiForm.API.DTOs.Responses;
using FlexiForm.API.Internals;
using FlexiForm.API.Models;

namespace FlexiForm.API.Mappings
{
    /// <summary>
    /// Defines AutoMapper mapping configurations for generating <see cref="LoginResponse"/>.
    /// </summary>
    public class LoginResponseProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginResponseProfile"/> class
        /// and configures the token and user mappings for login response.
        /// </summary>
        public LoginResponseProfile()
        {
            MapToken();
            MapUser();
        }

        /// <summary>
        /// Configures the mapping from <see cref="Token"/> to <see cref="LoginResponse"/>.
        /// </summary>
        private void MapToken()
        {
            CreateMap<Token, LoginResponse>()
                .ForMember(dest => dest.AccessToken, mapper => mapper.MapFrom(src => src.Value))
                .ForMember(dest => dest.ExpiresAt, mapper => mapper.MapFrom(src => src.ExpiresAt));
        }

        /// <summary>
        /// Configures the mapping from <see cref="User"/> to <see cref="LoginResponse"/>.
        /// </summary>
        private void MapUser()
        {
            CreateMap<User, LoginResponse>()
                .ForMember(dest => dest.User, mapper => mapper.MapFrom(src => src));
        }
    }
}
