using HotelFinalAPI.Application.IRepositories.IRoomRepos;
using HotelFinalAPI.Domain.Entities.DbEntities;
using HotelFinalAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Implementation.Services.Repositories.RoomRepos
{
    public class RoomWriteRepository : WriteRepository<Room>, IRoomWriteRepository//writeRepository den toredirem implementasiya olunmus olur, IRoomWriteRopository den toredirem loose coupled elemek ucun yeni dependency injection ile IRoomWriteRepository deyende RoomWriteRepository getirecek onun da implementasiyasi WriteRepository<> nin icerisindedir 
    {
        public RoomWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
