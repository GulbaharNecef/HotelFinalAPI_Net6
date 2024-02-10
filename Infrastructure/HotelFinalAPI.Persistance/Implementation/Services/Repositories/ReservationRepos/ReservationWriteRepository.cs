using HotelFinalAPI.Application.IRepositories.IReservationRepos;
using HotelFinalAPI.Domain.Entities.DbEntities;
using HotelFinalAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Implementation.Services.Repositories.ReservationRepos
{
    public class ReservationWriteRepository : WriteRepository<Reservation>, IReservationWriteRepository
    {
        public ReservationWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
