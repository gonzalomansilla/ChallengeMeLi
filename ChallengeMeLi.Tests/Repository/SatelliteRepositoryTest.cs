using ChallengeMeLi.Application.Repositories;
using ChallengeMeLi.Domain.Entities;
using ChallengeMeLi.Tests.Common;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeMeLi.Tests.Repository
{
	[ExcludeFromCodeCoverage]
	public class SatelliteRepositoryTest : ISatelliteRepository
	{
		private IList<Satellite> Satellites = new List<Satellite>();

		public SatelliteRepositoryTest()
		{
			Satellites.Add(new Satellite
			{
				Name = MockData.NAME_SATELLITE1,
				PosX = MockData.POS_X_SATELLITE1_DB,
				PosY = MockData.POS_Y_SATELLITE1_DB
			});
			Satellites.Add(new Satellite
			{
				Name = MockData.NAME_SATELLITE2,
				PosX = MockData.POS_X_SATELLITE2_DB,
				PosY = MockData.POS_Y_SATELLITE2_DB
			});
			Satellites.Add(new Satellite
			{
				Name = MockData.NAME_SATELLITE3,
				PosX = MockData.POS_X_SATELLITE3_DB,
				PosY = MockData.POS_Y_SATELLITE3_DB
			});
		}

		public async Task InsertAsync(Satellite entity)
		{
			Satellites.Add(entity);
			await Task.Delay(1);
		}

		public void Delete(Satellite entity)
		{
			Satellites.Remove(entity);
		}

		public async Task<bool> ExistAsync(string name)
		{
			return await Task.FromResult(Satellites.Any(e => e.Name == name));
		}

		public async Task<List<Satellite>> GetAllAsync()
		{
			return await Task.FromResult(Satellites.ToList());
		}

		public async Task<Satellite> GetByNameAsync(string name)
		{
			return await Task.FromResult(Satellites.FirstOrDefault(e => e.Name.Equals(name)));
		}

		public void Attach(Satellite entity)
		{
			throw new NotImplementedException();
		}

		public async Task SaveChangesAsync()
		{
			throw new NotImplementedException();
		}
	}
}
