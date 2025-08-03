
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Employee.Management.System.Data;
using Employee.Management.System.Extentions;
using Employee.Management.System.Helpers;
using Employee.Management.System.Profiles;
using Employee.Management.System.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace Employee.Management.System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddDbContext<StoreContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            //builder.Services.AddScoped<IEmployeeService, EmployeeService>();


             builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);


            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
                        builder.RegisterModule(new AutoFacModule()));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.TransactionMiddleware();

            MapperHelper.Mapper = app.Services.GetService<IMapper>()!;


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
