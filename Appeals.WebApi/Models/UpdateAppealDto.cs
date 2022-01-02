using Appeals.Application.Appeals.Commands.UpdateAppeal;
using Appeals.Application.Common.Mappings;
using AutoMapper;

namespace Appeals.WebApi.Models
{
    public class UpdateAppealDto : IMapWith<UpdateAppealCommand>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public void Mapping(Profile profile) 
        {
            profile.CreateMap<UpdateAppealDto, UpdateAppealCommand>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                ;
        }
    }
}
