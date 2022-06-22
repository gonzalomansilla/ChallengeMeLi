using ChallengeMeLi.Application.Repositories;
using ChallengeMeLi.Domain.Entities;
using ChallengeMeLi.Tests.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeMeLi.Tests.Repository
{
	public class SatelliteDataRepositoryTest : ISatelliteDataRepository
	{
		private IList<SatelliteData> SatellitesData = new List<SatelliteData>();

		public SatelliteDataRepositoryTest()
		{
			SatellitesData.Add(new SatelliteData
			{
				SatelliteDataId = 1,
				Distance = MockData.DISTANCE_SATELLITE1_REQ,
				Satellite = MockData.SatellitesDB[0]
			});
			SatellitesData.Add(new SatelliteData
			{
				SatelliteDataId = 2,
				Distance = MockData.DISTANCE_SATELLITE2_REQ,
				Satellite = MockData.SatellitesDB[1]
			});
			SatellitesData.Add(new SatelliteData
			{
				SatelliteDataId = 3,
				Distance = MockData.DISTANCE_SATELLITE3_REQ,
				Satellite = MockData.SatellitesDB[2]
			});
		}

		public async Task<bool> ExistAsync(string name)
		{
			return await Task.FromResult(SatellitesData.Any(e => e.Satellite.Name == name));
		}

		public async Task InsertAsync(SatelliteData entity)
		{
			SatellitesData.Add(entity);
			await Task.Delay(1);
		}

		public async Task SaveChangesAsync()
		{
			throw new NotImplementedException();
		}

		public void Attach(SatelliteData entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(SatelliteData entity)
		{
			throw new NotImplementedException();
		}

		public async Task<SatelliteData> GetLastSatelliteDataBySatelliteNameAsync(string name)
		{
			var lastSatelliteDataId = await Task.FromResult(
				SatellitesData
					.Where(m => m.Satellite.Name == name)
					.Max(m => m.SatelliteDataId));

			var message = await Task.FromResult(
			   SatellitesData.First(x => x.SatelliteDataId == lastSatelliteDataId));

			return message;
		}
	}
}
