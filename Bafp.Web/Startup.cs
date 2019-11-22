using System.Reflection;
using AutoMapper;
using Bafp.Logic.Database;
using Bafp.Logic.Services;
using Bafp.Web.Mapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bafp.Web
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var config = new AppConfig();
            Configuration.Bind(config);

            var factory = new ConnectionFactory(config.ConnectionStrings);

            services.AddSingleton<IConnectionFactory>(factory);
            services.AddTransient<ISpExecutor, SpExecutor>();
            services.AddTransient<ISmartExecutor, SmartExecutor>();

            services.AddMediatR(typeof(SpExecutor).GetTypeInfo().Assembly);

            var mapperConfiguration = new MapperConfiguration(m => { m.AddProfile<AppProfile>(); });

            services.AddSingleton(mapperConfiguration.CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
