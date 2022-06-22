using ChallengeMeLi.Application;
using ChallengeMeLi.Application.Repositories;
using ChallengeMeLi.Extensions;
using ChallengeMeLi.Tests.Repository;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace ChallengeMeLi.Tests
{
    public class StartupTest
    {
        public StartupTest(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Persistance
            services.AddTransient<ISatelliteRepository, SatelliteRepositoryTest>();
            services.AddTransient<ISatelliteDataRepository, SatelliteDataRepositoryTest>();
            services.AddTransient<IMessageRepository, MessageRepositoryTest>();

            // Application
            services.AddApplicationServices();

            services.AddControllers().AddApplicationPart(Assembly.Load("ChallengeMeLi")).AddControllersAsServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseErrorHandlindMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}