using AutoMapper;
using Bafp.Contracts;
using Bafp.Logic.Database;
using Bafp.Logic.Services;
using Bafp.Web.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Bafp.Web
{
    public class Startup
    {
        private static readonly Info Version = new Info { Version = "v1", Title = "Bafp API" };

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
            services.AddTransient<IDatabaseService, DatabaseService>();

            services.AddCors(options =>
            {
                options.AddPolicy(Constants.PolicyNames.AllowUi,
                    builder =>
                    {
                        foreach (var url in config.AllowedUrls)
                        {
                            builder.WithOrigins(url)
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        }
                    });
            });

            services.AddSwaggerGen(

                options =>
                {
                    options.DescribeAllEnumsAsStrings();
                    options.SwaggerDoc("v1", Version);
                });

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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
