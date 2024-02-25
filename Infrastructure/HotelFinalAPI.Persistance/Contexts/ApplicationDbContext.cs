using HotelFinalAPI.Domain.Entities.BaseEntities;
using HotelFinalAPI.Domain.Entities.DbEntities;
using HotelFinalAPI.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfiguration(new GuestConfiguration());  bir-bir configleri register etmek
            base.OnModelCreating(builder);

            //configuration fayllarini qeydiyyatdan kecirdim, bu sacn edir assembly ni IEntityTypeConfigurationdan torenmis any config clasini register edir
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
        }

        //bu method ne vaxt ki saveChanges metodu tetiklenecek(write repositorydeki) ilk once gelib buu override edilmis halini calisdiracaq sonra save edecek
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker : DbContext de bir propertydir. Entity ler uzerinden yapilan deyisikliklerin ve ya yeni eklenen verinin yakalanmasini saglayan property dir.Update Operasyonlarinda track edilen verileri yakalayip elde etmemizi saglar
            // bu islem intersektor adlanir, yeni insert ve ya updade islemlerinde araya girib gerekli datalari elave edecek
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.Now,
                    _ => DateTime.Now
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
