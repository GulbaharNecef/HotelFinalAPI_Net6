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
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Id).IsRequired().HasDefaultValueSql("NEWID()");
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Role).IsRequired().HasMaxLength(100);
        }
    }
}
