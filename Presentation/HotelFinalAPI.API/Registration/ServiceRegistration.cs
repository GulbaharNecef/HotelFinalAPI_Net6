namespace HotelFinalAPI.API.Registration
{
    public static class ServiceRegistration
    {
        public static void AddPresentationRegistration(this IServiceCollection services)
        {
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
