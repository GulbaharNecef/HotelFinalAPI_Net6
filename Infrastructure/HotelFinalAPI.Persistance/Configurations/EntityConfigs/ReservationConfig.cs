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
    public class ReservationConfig : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            //Model configuration
            builder.HasOne<Guest>(g => g.Guest)
                .WithMany(g => g.Reservations)
                .HasForeignKey(g => g.GuestId);

            //builder.HasKey(r => r.ReservationId);Id leri base de tutacam 

            //Room ve Rezervasiya arasinda one to many relationship
            builder.HasOne<Room>(g => g.Room)
                .WithMany(r => r.Reservations)
                .HasForeignKey(g => g.RoomId);

            //Property configuration
            //builder.Property(r => r.ReservationId).IsRequired();
            builder.Property(r => r.GuestId).IsRequired();
            builder.Property(r => r.RoomId).IsRequired();
            builder.Property(r => r.CheckInDate).IsRequired();
            builder.Property(r => r.CheckOutDate).IsRequired();


        }
    }
}
