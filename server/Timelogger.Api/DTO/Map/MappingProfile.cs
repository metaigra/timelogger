using AutoMapper;
using Timelogger.Entities;

namespace Timelogger.Api.DTO.Map
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();

            CreateMap<Interval, IntervalDto>();
            CreateMap<IntervalDto, Interval>();
        }
    }
}
