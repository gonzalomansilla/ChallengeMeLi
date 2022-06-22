using ChallengeMeLi.Application.Repositories;
using ChallengeMeLi.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/*
	Clase extensora que registra los servicios utilziados en la capa Persistence
 */

namespace ChallengeMeLi.Persistence
{
	public static class ServicesExtension
	{
		public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			// Database in memory
			//services.AddDbContext<AppDbContext>(option => option.UseInMemoryDatabase(databaseName: "InMemoryDb"));

			services.AddDbContext<AppDbContext>(option => option.UseSqlServer(
				configuration.GetConnectionString("DefaultConnection"),
				b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
			);

			services.AddTransient<ISatelliteRepository, SatelliteRepository>();
			services.AddTransient<ISatelliteDataRepository, SatelliteDataRepository>();
			services.AddTransient<IMessageRepository, MessageRepository>();
		}
	}
}
