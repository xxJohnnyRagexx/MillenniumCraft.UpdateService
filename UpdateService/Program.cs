using Data.Repositories;
using LiteDB;

namespace UpdateService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //var userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var userPath = @"D:\";
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<UpdatesRepository>();
            builder.Services.AddScoped<ILiteDatabase>(x =>
                new LiteDatabase(
                    Path.Combine(userPath,
                    builder.Configuration.GetSection("LiteDb")
                            .Get<LiteDbSettings>().Filename)));

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