using Appeals.Application.Common.Mappings;
using Appeals.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeals.Application.Appeals.Queries.GetAppealList
{
    public class AppealLookUpDto : IMapWith<Appeal>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }

        public void Mapping(Profile profile) 
        {
            profile.CreateMap<Appeal, AppealLookUpDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                ;
        }
    }
}
