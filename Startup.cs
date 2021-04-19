using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MtM.Repositories;
using MtM.Services;
using MySqlConnector;

namespace MtM
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MtM", Version = "v1" });
            });
            // REVIEW[epic=Authentication] creates functionality for authentication

      // REVIEW[epic=Authentication] creates functionality for hitting server from client
      services.AddCors(options =>
        {
          options.AddPolicy("CorsDevPolicy", builder =>
              {
                builder
                          .WithOrigins(new string[]{
                            "http://localhost:8080",
                            "http://localhost:8081"
                          })
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
              });
        });
        // REGISTER ALL TRANSIENTS HERE
        services.AddTransient<JobsRepository>();
        services.AddTransient<JobsService>();
        services.AddTransient<ContractorsRepository>();
        services.AddTransient<ContractorsService>();
        services.AddScoped<IDbConnection>(x => CreateDbConnection());
        }
        // MAKE BD CONNECTION HERE
        private IDbConnection CreateDbConnection()
    {
      string connectionString = Configuration["db:gearhost"];
      return new MySqlConnection(connectionString);
    }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MtM v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //ADD THIS LINE ABOVE AUTHORIZATION

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
