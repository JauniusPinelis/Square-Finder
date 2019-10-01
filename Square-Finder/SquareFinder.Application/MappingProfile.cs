using AutoMapper;
using SquareFinder.Core.Models;
using SquareFinder.Infrastructure.Entities;
using System;

namespace SquareFinder.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PointEntity, PointDto>();
            CreateMap<PointListEntity, PointListDto>();
        }
    }
}
