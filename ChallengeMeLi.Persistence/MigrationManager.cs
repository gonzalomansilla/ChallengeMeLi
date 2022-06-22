using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;

namespace ChallengeMeLi.Persistence
{
	/*
        Configuracion para ejecutar las migraciones automaticamente cuando se inicia el proyecto
     */

	public static class MigrationManager
	{
		public static IHost MigrateDatabase(this IHost host)
		{
			using (var scope = host.Services.CreateScope())
			{
				using (var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
				{
					try
					{
						appContext.Database.Migrate();

						appContext.Database.EnsureCreated();
						Console.WriteLine("Migraciones ejecutadas");
					}
					catch (Exception ex)
					{
						throw;
					}
				}
			}
			return host;
		}
	}
}
