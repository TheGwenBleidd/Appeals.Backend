using Appeals.Application.Common.Mappings;
using Appeals.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeals.Application.Appeals.Queries.GetAppealDetails
{
    public class AppealVm : IMapWith<Appeal>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Appeal, AppealVm>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                ;
        }
    }
}
