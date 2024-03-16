using FluentValidation.AspNetCore;
using HotelFinalAPI.API.Extensions;
using HotelFinalAPI.API.Registration;
using HotelFinalAPI.Application.AutoMapper;
using HotelFinalAPI.Application.Validators.Bills;
using HotelFinalAPI.Infrastructure.Filters;
using HotelFinalAPI.Infrastructure.Registrations;
using HotelFinalAPI.Persistance.Contexts;
using HotelFinalAPI.Persistance.Registration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using System.Security.Claims;
using System.Text;

namespace HotelFinalAPI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddPersistanceRegistration();
            builder.Services.AddPresentationRegistration();
            builder.Services.AddInfratructureRegistration();
            builder.Services.AddAutoMapper(typeof(MappingProfile));


            Logger log = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)// .GetSection("Serilog")
                .Enrich.FromLogContext()
                .CreateLogger();
            builder.Host.UseSerilog(log);//configures Serilog as the only provider, bypassing other providers in the pipeline.

            //builder.Logging.ClearProviders();
            //builder.Logging.AddSerilog(logger);adds a Serilog provider to the existing logging pipeline.


            //BillCreateDTO verirem o bu clasin oldugu assembly deki butun validasiya classlarina isaredir.Application bir assembly dir:)
            builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
                .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<BillCreateValidator>())
            .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            //todo bunu cixart registrationa , burda qalmasin, builder ile bagli error verecek => configuration.cs de handle et) 
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer("Admin", options =>
                options.TokenValidationParameters = new()
                {//bu applicationa her kimse token ile muraciet etse o tokende bu parametrler dogrulanacaq
                    ValidateAudience = true,//olusturulacak token degerini kimlerin, hansi sitelerin/originlerin kullanacagini belirlediyimiz deyer
                    ValidateIssuer = true,// olusturulacak token deyerini kimin dagittigini belirttiyimiz alandir
                    ValidateLifetime = true,//olusturulan token degerinin suresini kontroll eden dogrulamadir
                    ValidateIssuerSigningKey = true,//uretilen token in uygulamamiza aid bir deger oldugunu ifade eden security key verisinin dogrulanmasidir
                    ValidAudience = builder.Configuration["Token:Audience"],
                    ValidIssuer = builder.Configuration["Token:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                    LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,//to do see again jwt nin vaxti 5 deq uzanirdi, bununla 1 deq veriremse 1 deqe de bitir
                    NameClaimType = ClaimTypes.Name // JWT uzerinde name claimine karsilik gelen degeri User.Identity.Name propertisindenn elde ede biliriz
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            /*builder.Services.AddSwaggerGen(options =>
             {
                 options.SwaggerDoc("v1", new OpenApiInfo
                 {
                     Version = "v1",
                     Title = "HotelReservationAPI",
                     Description = "An ASP .NET Core Web API for reserving and managing hotel rooms"
                 });

                 options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()//Bearer schemesi ucun secDefinition elave etmek :|
                 {
                     In = ParameterLocation.Header,
                     Description = "Please insert JWT with Bearer into field",
                     Name = "Authorization",
                     BearerFormat = "JWT",
                     Type = SecuritySchemeType.Http,//ApiKey?
                     Scheme = "Bearer"//Http ve Scheme yazanda isledi amma ApiKey yazanda yox why?
                 });
                 options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                     {
                     new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                         }
                     },
                     new string[] {}
                     }
                 });
             });*/



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json/", "My Api");
                });
            }
            //Upload edende bu middleware olmalidir
            app.UseStaticFiles();

            app.UseSerilogRequestLogging();
            //app.UseExceptionHandler() -i extend etmisem)
            app.ConfigureExceptionHandler(app.Services.GetRequiredService<ILogger<Program>>());

            //logs HTTP request details (such as method, path, status code, and timing), should come before other middlewares like authentication, routing, bunu yazdim deye her endpoint de manually loglama etmeye ehtiyac qalmir.(oz custom mesajimi yazmaq istemiremse)
            //butun requestleri loglayir endpointin icinde _logger cagirmasamda cunki middlewaredi :)


            app.UseHttpLogging();

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
                LogContext.PushProperty("User_Name", username);
                await next(context);
            });

            app.MapControllers();

            app.Run();
        }
    }
}
