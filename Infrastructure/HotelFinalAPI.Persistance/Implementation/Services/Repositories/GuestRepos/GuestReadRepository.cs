using HotelFinalAPI.Application.IRepositories.IGuestRepos;
using HotelFinalAPI.Domain.Entities.DbEntities;
using HotelFinalAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Implementation.Services.Repositories.GuestRepos
{
    public class GuestReadRepository : ReadRepository<Guest>, IGuestReadRepository
    {
        public GuestReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
