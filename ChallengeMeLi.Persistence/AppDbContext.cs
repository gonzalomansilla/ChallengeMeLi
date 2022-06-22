using ChallengeMeLi.Domain.Entities;

using Microsoft.EntityFrameworkCore;

/*
	Clase responsable de interactuar con la base de datos
 */

namespace ChallengeMeLi.Persistence
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
		   : base(options)
		{
		}

		public DbSet<Satellite> Satellites { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Word> MessageWords { get; set; }
		public DbSet<SatelliteData> SatellitesData { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
		}
	}
}
