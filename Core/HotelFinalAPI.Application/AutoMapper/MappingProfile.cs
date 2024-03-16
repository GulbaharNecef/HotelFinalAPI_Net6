using AutoMapper;
using HotelFinalAPI.Application.DTOs.BillDTOs;
using HotelFinalAPI.Application.DTOs.EmployeeDTOs;
using HotelFinalAPI.Application.DTOs.GuestDTOs;
using HotelFinalAPI.Application.DTOs.ReservationDTOs;
using HotelFinalAPI.Application.DTOs.RoomDTOs;
using HotelFinalAPI.Application.DTOs.UserDTOs;
using HotelFinalAPI.Domain.Entities.DbEntities;
using HotelFinalAPI.Domain.Entities.IdentityEntities;
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
            CreateMap<Employee, EmployeeGetDTO>().ReverseMap();
            CreateMap<Guest, GuestGetDTO>().ReverseMap();
            CreateMap<Reservation, ReservationGetDTO>().ReverseMap();
            CreateMap<Room, RoomGetDTO>().ReverseMap();
            CreateMap<AppUser, UserGetDTO>().ReverseMap();


            //CreateMap<Bill, BillGetDTO>()
            //    .ForMember(dest => dest.BillId, act => act.MapFrom(src => src.Id))
            //    .ReverseMap();
            //For operations like updates and deletes, where performance is critical, minimizing unnecessary mappings can help improve performance.
        }
    }
}
