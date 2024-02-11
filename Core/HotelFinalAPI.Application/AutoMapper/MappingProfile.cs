using AutoMapper;
using HotelFinalAPI.Application.DTOs.BillDTOs;
using HotelFinalAPI.Domain.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HotelFinalAPI.Application.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Bill, BillGetDTO>().ReverseMap();

            //CreateMap<Bill, BillGetDTO>()
            //    .ForMember(dest => dest.BillId, act => act.MapFrom(src => src.Id))
            //    .ReverseMap();
            //For operations like updates and deletes, where performance is critical, minimizing unnecessary mappings can help improve performance.
        }
    }
}
