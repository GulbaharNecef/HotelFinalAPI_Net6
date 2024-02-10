using HotelFinalAPI.Application.IRepositories.IRoomRepos;
using HotelFinalAPI.Domain.Entities.DbEntities;
using HotelFinalAPI.Persistance.Contexts;
using HotelFinalAPI.Persistance.Repositories;
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
    }
}
