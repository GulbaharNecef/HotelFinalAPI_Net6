using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Configurations
{
    static class Configuration
    {
        public static string ConnectionString()
        {
            ConfigurationManager configurationManager = new();//Microsoft Extensions.Configuration nuget package

            // SetBasePath() metodu appsettings.json filenin pathini vermek ucun istifade edilir
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/HotelFinalAPI.API"));

            //Microsoft Extensions.Configuration.Json nuget package, AddJsonFile methodu basqa layerdeki(Presentation) .json filesine ulasmamizi sagliyor
            configurationManager.AddJsonFile("appsettings.json");
            return configurationManager.GetConnectionString("DefaultConnection");
        }
    }
}
