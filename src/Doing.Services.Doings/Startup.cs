using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doing.Common.Commands;
using Doing.Common.Mongo;
using Doing.Common.RabbitMq;
using Doing.Services.Doings.Domain.Repositories;
using Doing.Services.Doings.Handlers;
using Doing.Services.Doings.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Doing.Services.Doings
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Doing.Services.Doings", Version = "v1" });
            });


            services.AddMongoDb(Configuration);

            services.AddRabbitMq(Configuration);

            services
                .AddScoped<ICommandHandler<CreateDoing>, CreateDoingHandler>();

            services
                .AddScoped<ICategoryRepository, CategoryRepository>();
            
            services
                .AddScoped<IDoingRepository, DoingRepository>();

            services
                .AddScoped<IMongoDatabaseSeeder, CustomMongoSeeder>();
        
            services
                .AddScoped<IDoingService, DoingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Doing.Services.Doings v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ApplicationServices
                .GetService<IMongoDatabaseInitializer>()
                .InitializeAsync();
        }
    }
}
