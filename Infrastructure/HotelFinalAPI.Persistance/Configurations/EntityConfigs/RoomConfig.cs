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
    public class RoomConfig : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            //builder.HasKey(r => r.RoomId);
            //builder.Property(r => r.RoomId).IsRequired().HasMaxLength(256).HasColumnName("RoomId").HasColumnType(Guid.NewGuid().ToString());

            //builder.Property(r => r.RoomId).IsRequired();
            builder.Property(r => r.Id).IsRequired().HasDefaultValueSql("NEWID()");//id nin ssms de avtomatic artan(elave olunan) olmasi ucun
            builder.Property(r => r.RoomNumber).IsRequired().HasMaxLength(50);
            builder.Property(r => r.RoomType).IsRequired().HasMaxLength(50);
            builder.Property(r => r.Status).IsRequired().HasMaxLength(50);
            builder.Property(r => r.Price).IsRequired();
            builder.Property(r => r.CreatedDate).IsRequired();
            builder.Property(r => r.UpdatedDate).IsRequired();

        }
    }
}
