using HotelFinalAPI.Application.IRepositories.IBillRepos;
using HotelFinalAPI.Application.IRepositories.IEmployeeRepos;
using HotelFinalAPI.Application.IRepositories.IGuestRepos;
using HotelFinalAPI.Application.IRepositories.IReservationRepos;
using HotelFinalAPI.Application.IRepositories.IRoomRepos;
using HotelFinalAPI.Application.IUnitOfWorks;
using HotelFinalAPI.Persistance.Configurations;
using HotelFinalAPI.Persistance.Contexts;
using HotelFinalAPI.Persistance.Repositories.BillRepos;
using HotelFinalAPI.Persistance.Repositories.EmployeeRepos;
using HotelFinalAPI.Persistance.Repositories.GuestRepos;
using HotelFinalAPI.Persistance.Repositories.ReservationRepos;
using HotelFinalAPI.Persistance.Repositories.RoomRepos;
using HotelFinalAPI.Persistance.UnitOfWorks;
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

            services.AddScoped<IBillReadRepository, BillReadRepository>();
            services.AddScoped<IBillWriteRepository, BillWriteRepository>();

            services.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>();
            services.AddScoped<IEmployeeWriteRepository, EmployeeWriteRepository>();

            services.AddScoped<IGuestReadRepository, GuestReadRepository>();
            services.AddScoped<IGuestWriteRepository, GuestWriteRepository>();

            services.AddScoped<IReservationReadRepository, ReservationReadRepository>();
            services.AddScoped<IReservationWriteRepository, ReservationWriteRepository>();

            services.AddScoped<IRoomReadRepository, RoomReadRepository>();
            services.AddScoped<IRoomWriteRepository, RoomWriteRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
