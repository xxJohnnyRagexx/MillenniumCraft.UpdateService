using Data.Repositories;
using LiteDB;
using Shared;
using Shared.Models;

namespace UpdateService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var dbSettings = builder.Configuration.GetSection("LiteDb").Get<LiteDbSettings>();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<UpdatesRepository>();
            builder.Services.AddScoped<ILiteDatabase>(x =>
                new LiteDatabase(
                    SettingsExtension
                    .BuildDatabaseConnectionString(
                        dbSettings.Filename,
                        dbSettings.ConnectionMode)));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}