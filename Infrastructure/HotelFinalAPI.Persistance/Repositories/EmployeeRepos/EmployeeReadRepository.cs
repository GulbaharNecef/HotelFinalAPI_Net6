using HotelFinalAPI.Application.IRepositories.IEmployeeRepos;
using HotelFinalAPI.Domain.Entities.DbEntities;
using HotelFinalAPI.Persistance.Contexts;
using HotelFinalAPI.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Repositories.EmployeeRepos
{
    public class EmployeeReadRepository : ReadRepository<Employee>, IEmployeeReadRepository
    {
        public EmployeeReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
