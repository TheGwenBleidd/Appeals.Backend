using Appeals.Application.Appeals.Commands.CreateAppeal;
using Appeals.Application.Common.Mappings;
using AutoMapper;

namespace Appeals.WebApi.Models
{
    public class CreateAppealDto : IMapWith<CreateAppealCommand>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }

        public void Mapping(Profile profile) 
        { 
            profile.CreateMap<CreateAppealDto, CreateAppealCommand>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                ;
        }
    }
}
