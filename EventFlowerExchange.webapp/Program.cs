using EventFlowerExchange.Repositories.Entities;
using EventFlowerExchange.Repositories.Interfaces;
using EventFlowerExchange.Repositories.Repositories;
using EventFlowerExchange.services.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace EventFlowerExchange
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            //Add Dbcontext
            builder.Services.AddDbContext<EventFlowerExchangeContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //DI Service flower
            builder.Services.AddScoped<IFlowerService, FlowerService>();

            //DI Responsitories flower
            builder.Services.AddScoped<IFlowerRepository, FlowerRepository>();

            //DI Service
            builder.Services.AddScoped<IUserService, UserService>();

            //DI Responsitories
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add Authorization
            builder.Services.AddAuthorization();


            builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

            // Add CORS (Cross-Origin Resource Sharing)
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin() // Cập nhật địa chỉ frontend
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


            var app = builder.Build();

            // Use CORS Middleware
            app.UseCors();

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
