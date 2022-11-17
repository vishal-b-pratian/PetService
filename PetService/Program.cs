using PetService.Data;
using PetService.Data.RepositoriesEFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PetService.Domain.Repositories;
using PetService.Domain.Managers;

namespace PetService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<PetServicesDbContext>(options =>
                options.UseSqlServer(connectionString));


            builder.Services.AddScoped<IPetManager, PetManager>();
            builder.Services.AddScoped<IPetOwnerManager, PetOwnerManager>();
            builder.Services.AddScoped<IPetRepository, PetRepository>();
            builder.Services.AddScoped<IPetOwnerRepository, PetOwnerRepository>();
            builder.Services.AddScoped<PetServicesDbContext, PetServicesDbContext>();
            builder.Services.AddScoped<IMemoryCache, MemoryCache>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(p => p.AddPolicy("corsPolicy", build =>
            {
                build.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
            }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("corsPolicy");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
