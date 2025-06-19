using AutoMapper;
using FlexiForm.API.DTOs.Requests;
using FlexiForm.API.DTOs.Responses;

namespace FlexiForm.API.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping <see cref="RegistrationRequest"/> to <see cref="RegistrationResponse"/>.
    /// </summary>
    public class RegistrationResponseProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationResponseProfile"/> class.
        /// </summary>
        public RegistrationResponseProfile()
        {
            MapRegistrationRequest();
        }

        /// <summary>
        /// Configures the mapping from <see cref="RegistrationRequest"/> to <see cref="RegistrationResponse"/>.
        /// </summary>
        private void MapRegistrationRequest()
        {
            CreateMap<RegistrationRequest, RegistrationResponse>()
                .ForMember(dest => dest.FirstName, mapper => mapper.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, mapper => mapper.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, mapper => mapper.MapFrom(src => src.Email))
                .ForMember(dest => dest.Gender, mapper => mapper.MapFrom(src => src.Gender));
        }
    }
}
