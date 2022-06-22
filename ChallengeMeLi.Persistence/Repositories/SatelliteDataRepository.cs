using ChallengeMeLi.Application.Repositories;
using ChallengeMeLi.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeMeLi.Persistence.Repositories
{
	public class SatelliteDataRepository : ISatelliteDataRepository
	{
		private readonly AppDbContext _context;

		public SatelliteDataRepository(AppDbContext context)
		{
			_context = context;
		}

		public void Attach(SatelliteData entity)
		{
			_context.Attach(entity).State = EntityState.Modified;
		}

		public void Delete(SatelliteData entity)
		{
			_context.Remove(entity);
		}

		public async Task<bool> ExistAsync(string name)
		{
			return await _context.SatellitesData.AnyAsync(e => e.Satellite.Name == name);
		}

		public async Task<SatelliteData> GetLastSatelliteDataBySatelliteNameAsync(string name)
		{
			var lastSatelliteDataId = await _context.SatellitesData
				.Where(m => m.Satellite.Name == name)
				.MaxAsync(m => m.SatelliteDataId);

			var satelliteData = await _context.SatellitesData
				.FirstAsync(x => x.SatelliteDataId == lastSatelliteDataId);

			return satelliteData;
		}

		public async Task InsertAsync(SatelliteData entity)
		{
			await _context.SatellitesData.AddAsync(entity);
			await SaveChangesAsync();
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
