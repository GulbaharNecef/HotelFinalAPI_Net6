using HotelFinalAPI.Application.Exceptions;
using HotelFinalAPI.Application.Exceptions.CommonExceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace HotelFinalAPI.API.Extensions
{
    public static class ConfigureExceptionHandlerExtension
    {
        //webApplication turune bir extension olacaq
        public static void ConfigureExceptionHandler<T>(this WebApplication application, ILogger<T> logger)
        {//UseExceptionHandler middlewareni burda cagiriram configurasiya edirem
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json; //"application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature != null)
                    {
                        //logging
                        logger.LogError("Xeta bas verdi " + contextFeature.Error.Message);

                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            InvalidIdFormatException => StatusCodes.Status400BadRequest,
                            AuthenticationErrorException => StatusCodes.Status400BadRequest,
                            ArgumentNullException => StatusCodes.Status400BadRequest,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        await context.Response.WriteAsync(JsonSerializer.Serialize(new // new ile anonim yaziriq ErrorModel clasi yaratmaq da possible dir
                        {
                           // StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Title = "Error happened!"
                        }));
                    }
                });
            });
        }
    }
}
