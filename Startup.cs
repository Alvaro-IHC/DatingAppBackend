using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DatingApp.API.Hubs;

// Conexión with MySQL
// using MySQL.Data.EntityFrameworkCore.Extensions;

namespace DatingApp.API
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
            // Conexion with Sqlite
            // services.AddDbContext<DataContext>(x => {
            //     x.UseSqlite(Configuration.GetConnectionString("sqliteconnection"));
            // });

            // Conexion with Sql Server 
            services.AddDbContext<DataContext>(x => {
                x.UseSqlServer(Configuration.GetConnectionString("sqlserverconnection"));
            });

            // Conexión with MySQL
            // services.AddDbContext<DataContext>(x => {
            //     x.UseMySQL(Configuration.GetConnectionString("mysqlconnectionUpdate-Database"));
            // });
            services.AddControllers();
            services.AddSignalR();
            services.AddMvc();
            services.AddCors(options =>
            {
                options.AddPolicy("ClientPermission", policy => 
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:3000")
                        .AllowCredentials();
                });
            });
            services.AddScoped<IAuthRepository, AuthRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) 
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            // app.UseCors(x => 
            //     x.AllowAnyHeader()
            //     .AllowAnyMethod()
            //     .AllowAnyOrigin()
            //     .AllowCredentials()
            // );
            // Returns the request to the client
            // app.UseMvc();
            app.UseCors("ClientPermission");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/hubs/chat");
            });

        }
    }
}
