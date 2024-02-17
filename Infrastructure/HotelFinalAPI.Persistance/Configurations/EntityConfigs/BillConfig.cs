﻿using HotelFinalAPI.Domain.Entities.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Configurations.EntityConfigs
{
    public class BillConfig : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            //Relationship Configuration ve ya Entity configuration
            builder.HasOne<Guest>(b => b.Guest)
                .WithMany(g => g.Bills)
                .HasForeignKey(g => g.GuestId);
                //.OnDelete(DeleteBehavior.Cascade);//bu table silinse bununla elaqeli butun tableler silinecek deye anladim
                //default olaraq cascade dir

            builder.Property(b => b.GuestId).IsRequired();
            builder.Property(b => b.Amount).IsRequired();//.HasColumnType("decimal(10,2)");
                                                         //default decimal(18,2) olacaq, 18 precision,
                                                         //2 digits to the right of the decimal point 
            builder.Property(b => b.PaidStatus).IsRequired();
        }
    }
}
