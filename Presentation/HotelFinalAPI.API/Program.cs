using HotelFinalAPI.API.Extensions;
using HotelFinalAPI.Application.AutoMapper;
using HotelFinalAPI.Persistance.Contexts;
using HotelFinalAPI.Persistance.Registration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;

namespace HotelFinalAPI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddPersistanceRegistration();

            builder.Services.AddAutoMapper(typeof(MappingProfile));


            Logger log = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)// .GetSection("Serilog")
                .Enrich.FromLogContext()
                .CreateLogger();
            builder.Host.UseSerilog(log);//configures Serilog as the only provider, bypassing other providers in the pipeline.

            //builder.Logging.ClearProviders();
            //builder.Logging.AddSerilog(logger);adds a Serilog provider to the existing logging pipeline.



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            

            //app.UseExceptionHandler();
            app.ConfigureExceptionHandler(app.Services.GetRequiredService<ILogger<Program>>());

            //logs HTTP request details (such as method, path, status code, and timing), should come before other middlewares like authentication, routing, bunu yazdim deye her endpoint de manually loglama etmeye ehtiyac qalmir.(oz custom mesajimi yazmaq istemiremse)
            //butun requestleri loglayir endpointin icinde _logger cagirmasamda cunki middlewaredi :)
            app.UseSerilogRequestLogging();
            
            app.UseHttpLogging();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
