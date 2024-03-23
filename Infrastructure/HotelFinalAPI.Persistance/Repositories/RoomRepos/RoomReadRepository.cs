using HotelFinalAPI.Application.Helpers;
using HotelFinalAPI.Application.IRepositories.IRoomRepos;
using HotelFinalAPI.Domain.Entities.DbEntities;
using HotelFinalAPI.Domain.Enums;
using HotelFinalAPI.Persistance.Contexts;
using HotelFinalAPI.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Repositories.RoomRepos
{
    public class RoomReadRepository : ReadRepository<Room>, IRoomReadRepository
    {
        public RoomReadRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<Room> GetFiltered(QueryObject query, bool tracking = true)
        {
            var rooms = Table.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.RoomType) && Enum.TryParse(query.RoomType, out RoomTypes roomType))
            {
                rooms = Table.Where(r => (r.RoomType == roomType));
            }
            if(!string.IsNullOrWhiteSpace(query.Status) && Enum.TryParse(query.Status, out RoomStatus status))
            {
                rooms = Table.Where(r=>r.Status == status);
            }
            if(query.MinPrice is not null)
            {
                rooms = Table.Where(r => r.Price > query.MinPrice);
            }
            if(query.MaxPrice is not null)
            {
                rooms = Table.Where(r => r.Price < query.MaxPrice);
            }
            return rooms;
        }
    }
}
