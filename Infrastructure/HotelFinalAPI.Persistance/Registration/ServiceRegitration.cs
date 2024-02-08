using HotelFinalAPI.Persistance.Configurations;
using HotelFinalAPI.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Registration
{
    public static class ServiceRegitration
    {
        public static void AddPersistanceRegistration(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
            option.UseSqlServer(Configuration.ConnectionString()));
        }
    }
}
