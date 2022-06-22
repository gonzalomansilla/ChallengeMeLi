using ChallengeMeLi.Domain.Entities;

using System.Threading.Tasks;

namespace ChallengeMeLi.Application.Repositories
{
	public interface ISatelliteDataRepository
	{
		Task<SatelliteData> GetLastSatelliteDataBySatelliteNameAsync(string name);

		void Delete(SatelliteData entity);

		Task InsertAsync(SatelliteData entity);

		Task<bool> ExistAsync(string name);

		Task SaveChangesAsync();

		void Attach(SatelliteData entity);
	}
}
