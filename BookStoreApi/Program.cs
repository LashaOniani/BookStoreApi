
using BookStoreApi.Packages;

namespace BookStoreApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IPKG_Bookstore, PKG_Bookstore>();
            builder.Services.AddScoped<IPKG_Orders, PKG_Orders>();

            builder.Services.AddCors(config =>
            {
                config.AddPolicy("AllowAllCors", options =>
                {
                    options.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAllCors");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
