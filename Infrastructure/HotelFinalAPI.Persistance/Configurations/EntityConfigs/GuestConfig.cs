using HotelFinalAPI.Domain.Entities.DbEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Configurations.EntityConfigs
{
    public class GuestConfig : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            //Reservation Many side oldugu ucun orda eledim
            //builder.HasMany<Reservation>(g => g.Reservations)
            //    .WithOne(r => r.Guest);

            //Bill many side oldugu ucun orda etmisem
            //builder.HasMany<Bill>(r => r.Bills)
            //    .WithOne(g => g.Guest);

            builder.Property(g => g.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(g => g.LastName).IsRequired().HasMaxLength(50);
            builder.Property(g => g.Email).IsRequired().HasMaxLength(100);
            builder.Property(g => g.Phone).IsRequired().HasMaxLength(20);
        }
    }
}
