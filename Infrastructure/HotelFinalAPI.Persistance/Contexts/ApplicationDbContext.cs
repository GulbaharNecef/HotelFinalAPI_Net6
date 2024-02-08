using HotelFinalAPI.Domain.Entities.DbEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Bill> Bills {  get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfiguration(new GuestConfiguration());  bir-bir configleri register etmek
            base.OnModelCreating(builder);

            //configuration fayllarini qeydiyyatdan kecirdim, bu sacn edir assembly ni IEntityTypeConfigurationdan torenmis any config clasini register edir
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
        }
    }
}
