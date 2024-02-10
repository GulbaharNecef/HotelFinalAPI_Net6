using HotelFinalAPI.Application.IRepositories.IReservationRepos;
using HotelFinalAPI.Domain.Entities.DbEntities;
using HotelFinalAPI.Persistance.Contexts;
using HotelFinalAPI.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Repositories.ReservationRepos
{
    public class ReservationReadRepository : ReadRepository<Reservation>, IReservationReadRepository
    {
        public ReservationReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
