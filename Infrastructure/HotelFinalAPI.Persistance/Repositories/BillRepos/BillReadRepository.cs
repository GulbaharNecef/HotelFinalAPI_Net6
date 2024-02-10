using HotelFinalAPI.Application.IRepositories.IBillRepos;
using HotelFinalAPI.Domain.Entities.DbEntities;
using HotelFinalAPI.Persistance.Contexts;
using HotelFinalAPI.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Repositories.BillRepos
{
    public class BillReadRepository : ReadRepository<Bill>, IBillReadRepository
    {
        public BillReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
