using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace HotelFinalAPI.API.Registration
{
    public static class ServiceRegistration
    {
        
        public static void AddPresentationRegistration(this IServiceCollection services)
        {
            //addAuthentication
            services.AddSwaggerGen(options =>
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
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            
        }

    }
}
