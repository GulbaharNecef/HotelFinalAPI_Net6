﻿using HotelFinalAPI.Persistance.Configurations;
using HotelFinalAPI.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {// bu class bize migrationlari powerShell uzerinden etmeyimize imkan verir

            //ConfigurationManager configurationManager = new();//Microsoft Extensions.Configuration nuget package

            //// SetBasePath() metodu appsettings.json filenin pathini vermek ucun istifade edilir
            //configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),"../../Presentation/HotelFinalAPI.API"));

            ////Microsoft Extensions.Configuration.Json nuget package, AddJsonFile methodu basqa layerdeki(Presentation) .json filesine ulasmamizi sagliyor
            //configurationManager.AddJsonFile("appsettings.json");

            DbContextOptionsBuilder<ApplicationDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer(Configuration.ConnectionString());
            return new (dbContextOptionsBuilder.Options);
        }
    }
}